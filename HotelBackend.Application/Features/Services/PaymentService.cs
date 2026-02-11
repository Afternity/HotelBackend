using AutoMapper;
using FluentValidation;
using HotelBackend.Domain.Enums.PaymentEnums;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.DeletePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.GetPaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.UpdatePaymentDTOs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentListVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class PaymentService
        : IPaymentService
    {
        // БД contracts
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;
        // CUD validators
        private readonly IValidator<CreatePaymentDto> _createPaymentDtoValidator;
        private readonly IValidator<UpdatePaymentDto> _updatePaymentDtoValidator;
        private readonly IValidator<HardDeletePaymentDto> _hardDeletePaymentDtoValidator;
        private readonly IValidator<SoftDeletePaymentDto> _softDeletePaymentDtoValidator;
        // R validators
        private readonly IValidator<GetPaymentDto> _getPaymentDtoValidator;
        private readonly IValidator<GetAllByUserPaymentDto> _getAllByUserPaymentDto;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository,
            IMapper mapper,
            ILogger<PaymentService> logger,
            IValidator<CreatePaymentDto> createPaymentDtoValidator,
            IValidator<UpdatePaymentDto> updatePaymentDtoValidator,
            IValidator<HardDeletePaymentDto> hardDeletePaymentDtoValidator,
            IValidator<SoftDeletePaymentDto> softDeletePaymentDtoValidator,
            IValidator<GetPaymentDto> getPaymentDtoValidator,
            IValidator<GetAllByUserPaymentDto> getAllByUserPaymentDto)
        {
            // БД contracts
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createPaymentDtoValidator = createPaymentDtoValidator;
            _updatePaymentDtoValidator = updatePaymentDtoValidator;
            _hardDeletePaymentDtoValidator = hardDeletePaymentDtoValidator;
            _softDeletePaymentDtoValidator = softDeletePaymentDtoValidator;
            // R validators
            _getPaymentDtoValidator = getPaymentDtoValidator;
            _getAllByUserPaymentDto = getAllByUserPaymentDto;
        }

        public async Task<Guid> CreateAsync(
            CreatePaymentDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания Payment.");

            var validation = await _createPaymentDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var booking = await _bookingRepository
                .GetByIdAsync(createDto.BookingId, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {createDto.BookingId}");
                return Guid.Empty;
            }

            var newPayment = new Payment()
            {
                Id = Guid.NewGuid(),

                TotalAmount = createDto.TotalAmount,
                PaymentDate = createDto.PaymentDate,
                PaymentMethod = (PaymentMethod)createDto.PaymentMethod,
                Status = (PaymentStatus)createDto.Status,

                BookingId = createDto.BookingId,

                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _paymentRepository
                .CreateAsync(newPayment, cancellationToken);

            _logger.LogInformation($"Payment создан с Id: {newPayment.Id}");

            return newPayment.Id;
        }

        public async Task<PaymentListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения Payments, где IsDeleted == false");

            var payments = await _paymentRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("Payments, где IsDeleted == false, получены");

            return new PaymentListVm()
            {
                PaymentLookups = _mapper.Map<IList<PaymentLookupDto>>(payments)
            };
        }

        public async Task<UserPaymentListVm> GetAllByUserAsync(
            GetAllByUserPaymentDto getAllDto, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получение Payments, где IsDeleted == false , определенного User");

            var validation = await _getAllByUserPaymentDto
                .ValidateAsync(getAllDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var payments = await _paymentRepository
                .GetAllByUserAsync(getAllDto.UserId, cancellationToken);

            _logger.LogInformation("Payments, где IsDeleted == false, получены");

            return new UserPaymentListVm()
            {
                UserPaymentLookups = _mapper.Map<IList<UserPaymentLookupDto>>(payments)
            };
        }

        public async Task<PaymentDetailsVm?> GetByIdAsync(
            GetPaymentDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Payment по Id: {getDto.Id}");

            var validation = await _getPaymentDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var payment = await _paymentRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (payment == null)
            {
                _logger.LogWarning($"Payment не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"Payment найден по Id: {getDto.Id}");

            return _mapper.Map<PaymentDetailsVm>(payment);
        }

        public async Task HardDeleteAsync(
            HardDeletePaymentDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления Payment по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeletePaymentDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var payment = await _paymentRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (payment == null)
            {
                _logger.LogWarning($"Payment не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            await _paymentRepository
                .HardDeleteAsync(payment, cancellationToken);

            _logger.LogInformation($"Payment полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeletePaymentDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления Payment по Id: {softDeleteDto.Id}");

            var validation = await _softDeletePaymentDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var payment = await _paymentRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (payment == null)
            {
                _logger.LogWarning($"Payment не найден по Id: {softDeleteDto.Id}");
                return;
            }

            payment.DeletedAt = DateTime.UtcNow;
            payment.IsDeleted = true;

            await _paymentRepository
                .SoftDeleteAsync(payment, cancellationToken);

            _logger.LogInformation($"Payment мягко удалён по Id: {softDeleteDto.Id}");
        }

        public async Task UpdateAsync(
            UpdatePaymentDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало обновления Payment по Id: {updateDto.Id}");

            var validation = await _updatePaymentDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var booking = await _bookingRepository
                .GetByIdAsync(updateDto.BookingId, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {updateDto.BookingId}");
                return;
            }

            var payment = await _paymentRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (payment == null)
            {
                _logger.LogWarning($"Payment не найден по Id: {updateDto.Id}");
                return;
            }

            payment.TotalAmount = updateDto.TotalAmount;
            payment.PaymentDate = updateDto.PaymentDate;
            payment.PaymentMethod = (PaymentMethod)updateDto.PaymentMethod;
            payment.Status = (PaymentStatus)updateDto.Status;
            payment.BookingId = updateDto.BookingId;

            payment.UpdatedAt = DateTime.UtcNow;

            await _paymentRepository
                .UpdateAsync(payment, cancellationToken);

            _logger.LogInformation($"Payment обновлён по Id: {updateDto.Id}");
        }
    }
}
