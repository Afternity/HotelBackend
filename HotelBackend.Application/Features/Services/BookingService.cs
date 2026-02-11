using AutoMapper;
using FluentValidation;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class BookingService
        : IBookingService
    {
        // БД contracts
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;
        // CUD validators
        private readonly IValidator<CreateBookingDto> _createBookingDtoValidator;
        private readonly IValidator<UpdateBookingDto> _updateBookingDtoValidator;
        private readonly IValidator<HardDeleteBookingDto> _hardDeleteBookingDtoValidator;
        private readonly IValidator<SoftDeleteBookingDto> _softDeleteBookingDtoValidator;
        // R validators
        private readonly IValidator<GetBookingDto> _getBookingDtoValidator;
        private readonly IValidator<GetLastUserBookingDto> _getLastUserBookingDtoValidator;
        private readonly IValidator<GetUserBookingsDto> _getUserBookingsDtoValidator;

        public BookingService(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IMapper mapper,
            ILogger<BookingService> logger,
            IValidator<CreateBookingDto> createBookingDtoValidator,
            IValidator<UpdateBookingDto> updateBookingDtoValidator,
            IValidator<HardDeleteBookingDto> hardDeleteBookingDtoValidator,
            IValidator<SoftDeleteBookingDto> softDeleteBookingDtoValidator,
            IValidator<GetBookingDto> getBookingDtoValidator,
            IValidator<GetLastUserBookingDto> getLastUserBookingDtoValidator,
            IValidator<GetUserBookingsDto> getUserBookingsDtoValidator)
        {
            // БД contracts
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createBookingDtoValidator = createBookingDtoValidator;
            _updateBookingDtoValidator = updateBookingDtoValidator;
            _hardDeleteBookingDtoValidator = hardDeleteBookingDtoValidator;
            _softDeleteBookingDtoValidator = softDeleteBookingDtoValidator;
            // R validators
            _getBookingDtoValidator = getBookingDtoValidator;
            _getLastUserBookingDtoValidator = getLastUserBookingDtoValidator;
            _getUserBookingsDtoValidator = getUserBookingsDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateBookingDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания Booking.");

            var validation = await _createBookingDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(createDto.RoomId, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {createDto.RoomId}");
                return Guid.Empty;
            }

            var newBooking = new Booking()
            {
                Id = Guid.NewGuid(),

                CheckInDate = createDto.CheckInDate,
                CheckOutDate = createDto.CheckOutDate,

                UserId = createDto.UserId,
                RoomId = createDto.RoomId,

                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _bookingRepository
                .CreateAsync(newBooking, cancellationToken);

            _logger.LogInformation($"Бронирование создано с Id: {newBooking.Id}");

            return newBooking.Id;
        }

        public async Task<BookingListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получение Bookings, где IsDeleted == false");

            var bookings = await _bookingRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("Booking, где IsDeleted == false, получены");

            return new BookingListVm()
            {
                BookingLookups = _mapper.Map<IList<BookingLookupDto>>(bookings)
            };
        }

        public async Task<UserBookingListVm> GetAllByUserAsync(
            GetUserBookingsDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получение Bookings определенного User, где IsDeleted == false");

            var validation = await _getUserBookingsDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var bookings = await _bookingRepository
                .GetAllByUserAsync(getDto.UserId, cancellationToken);

            _logger.LogInformation("Bookings определенного User, где IsDeleted == false, получены");

            return new UserBookingListVm()
            {
                UserBookingLookups = _mapper.Map<IList<UserBookingLookupDto>>(bookings)
            };
        }

        public async Task<BookingDetailsVm?> GetByIdAsync(
            GetBookingDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Booking по Id: {getDto.Id}");

            var validation = await _getBookingDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var booking = await _bookingRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"Booking найден по Id: {getDto.Id}");

            return _mapper.Map<BookingDetailsVm>(booking);
        }

        public async Task<BookingDetailsVm?> GetLastBookingByUserAsync(
            GetLastUserBookingDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения последнего Booking определенного User");

            var validation = await _getLastUserBookingDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var lastBooking = await _bookingRepository
                .GetLastBookingByUserAsync(getDto.UserId, cancellationToken);

            if (lastBooking == null)
            {
                _logger.LogWarning($"Последний Booking у User не найден");
                return null;
            }

            _logger.LogInformation($"Последний Booking определенного пользователя найден: {lastBooking.Id}");

            return _mapper.Map<BookingDetailsVm>(lastBooking);
        }

        public async Task HardDeleteAsync(
            HardDeleteBookingDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления Booking по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeleteBookingDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var booking = await _bookingRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            await _bookingRepository
                .HardDeleteAsync(booking, cancellationToken);

            _logger.LogInformation($"Booking полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeleteBookingDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления Booking по Id: {softDeleteDto.Id}");

            var validation = await _softDeleteBookingDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var booking = await _bookingRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {softDeleteDto.Id}");
                return;
            }

            booking.DeletedAt = DateTime.UtcNow;
            booking.IsDeleted = true;

            await _bookingRepository
                .SoftDeleteAsync(booking, cancellationToken);

            _logger.LogInformation($"Booking мягко удалён по Id: {softDeleteDto.Id}");
        }

        public async Task UpdateAsync(
            UpdateBookingDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного обновления Booking по Id: {updateDto.Id}");

            var validation = await _updateBookingDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var updateBooking = await _bookingRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (updateBooking == null)
            {
                _logger.LogWarning($"Booking не найдет по Id: {updateDto.Id}");
                return;
            }

            var room = await _roomRepository
                .GetByIdAsync(updateDto.RoomId, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {updateDto.RoomId}");
                return;
            }

            updateBooking.CheckInDate = updateDto.CheckInDate;
            updateBooking.CheckOutDate = updateDto.CheckOutDate;

            updateBooking.UserId = updateDto.UserId;
            updateBooking.RoomId = updateDto.RoomId;

            updateBooking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository
                .UpdateAsync(updateBooking, cancellationToken);

            _logger.LogInformation($"Booking полностью обновлён по Id: {updateDto.Id}");
        }
    }
}
