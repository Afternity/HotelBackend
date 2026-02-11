using AutoMapper;
using FluentValidation;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewListVMs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewLookupDTOs;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class ReviewService
        : IReviewService
    {
        // БД contracts
        private readonly IReviewRepository _reviewRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;
        // CUD validators
        private readonly IValidator<CreateReviewDto> _createReviewDtoValidator;
        private readonly IValidator<UpdateReviewDto> _updateReviewDtoValidator;
        private readonly IValidator<HardDeleteReviewDto> _hardDeleteReviewDtoValidator;
        private readonly IValidator<SoftDeleteReviewDto> _softDeleteReviewDtoValidator;
        // R validators
        private readonly IValidator<GetReviewDto> _getReviewDtoValidator;
        private readonly IValidator<GetAllByRatingAndRoomReviewDto> _getAllByRatingAndRoomReviewDtoValidator;

        public ReviewService(
            IReviewRepository reviewRepository,
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository,
            IMapper mapper,
            ILogger<ReviewService> logger,
            IValidator<CreateReviewDto> createReviewDtoValidator,
            IValidator<UpdateReviewDto> updateReviewDtoValidator,
            IValidator<HardDeleteReviewDto> hardDeleteReviewDtoValidator,
            IValidator<SoftDeleteReviewDto> softDeleteReviewDtoValidator,
            IValidator<GetReviewDto> getReviewDtoValidator,
            IValidator<GetAllByRatingAndRoomReviewDto> getAllByRatingAndRoomReviewDtoValidator)
        {
            // БД contracts
            _reviewRepository = reviewRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createReviewDtoValidator = createReviewDtoValidator;
            _updateReviewDtoValidator = updateReviewDtoValidator;
            _hardDeleteReviewDtoValidator = hardDeleteReviewDtoValidator;
            _softDeleteReviewDtoValidator = softDeleteReviewDtoValidator;
            // R validators
            _getReviewDtoValidator = getReviewDtoValidator;
            _getAllByRatingAndRoomReviewDtoValidator = getAllByRatingAndRoomReviewDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateReviewDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания Review.");

            var validation = await _createReviewDtoValidator
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

            var newReview = new Review()
            {
                Id = Guid.NewGuid(),

                Rating = createDto.Rating,
                Text = createDto.Text, 

                BookingId = createDto.BookingId,

                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _reviewRepository
                .CreateAsync(newReview, cancellationToken);

            _logger.LogInformation($"Review создан с Id: {newReview.Id}");

            return newReview.Id;
        }

        public async Task<ReviewListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения Reviews, где IsDeleted == false");

            var reviews = await _reviewRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("Reviews, где IsDeleted == false, получены");

            return new ReviewListVm()
            {
                ReviewLookups = _mapper.Map<IList<ReviewLookupDto>>(reviews)
            };
        }

        public async Task<RatingAndRoomReviewListVm> GetAllByRatingAndRoomAsync(
            GetAllByRatingAndRoomReviewDto getAllDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Reviews по рейтингу {getAllDto.Rating} и комнате {getAllDto.RoomId}");

            var validation = await _getAllByRatingAndRoomReviewDtoValidator
                .ValidateAsync(getAllDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(getAllDto.RoomId, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {getAllDto.RoomId}");
                return new RatingAndRoomReviewListVm();
            }

            var reviews = await _reviewRepository
                .GetAllByRatingAndRoomAsync(getAllDto.Rating, room, cancellationToken);

            _logger.LogInformation($"Reviews по рейтингу {getAllDto.Rating} и комнате {getAllDto.RoomId} получены");

            return new RatingAndRoomReviewListVm()
            {
                RatingAndRoomReviewLookups = _mapper.Map<IList<RatingAndRoomReviewLookupDto>>(reviews)
            };
        }

        public async Task<ReviewDetailsVm?> GetByIdAsync(
            GetReviewDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Review по Id: {getDto.Id}");

            var validation = await _getReviewDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var review = await _reviewRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (review == null)
            {
                _logger.LogWarning($"Review не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"Review найден по Id: {getDto.Id}");

            return _mapper.Map<ReviewDetailsVm>(review);
        }

        public async Task HardDeleteAsync(
            HardDeleteReviewDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления Review по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeleteReviewDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var review = await _reviewRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (review == null)
            {
                _logger.LogWarning($"Review не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            await _reviewRepository
                .HardDeleteAsync(review, cancellationToken);

            _logger.LogInformation($"Review полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeleteReviewDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления Review по Id: {softDeleteDto.Id}");

            var validation = await _softDeleteReviewDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var review = await _reviewRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (review == null)
            {
                _logger.LogWarning($"Review не найден по Id: {softDeleteDto.Id}");
                return;
            }

            review.DeletedAt = DateTime.UtcNow;
            review.IsDeleted = true;

            await _reviewRepository
                .SoftDeleteAsync(review, cancellationToken);

            _logger.LogInformation($"Review мягко удалён по Id: {softDeleteDto.Id}");
        }

        public async Task UpdateAsync(
            UpdateReviewDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало обновления Review по Id: {updateDto.Id}");

            var validation = await _updateReviewDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var review = await _reviewRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (review == null)
            {
                _logger.LogWarning($"Review не найден по Id: {updateDto.Id}");
                return;
            }

            var booking = await _bookingRepository
                .GetByIdAsync(updateDto.BookingId, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning($"Booking не найден по Id: {updateDto.BookingId}");
                return;
            }

            review.Rating = updateDto.Rating;
            review.Text = updateDto.Text;

            review.BookingId = updateDto.BookingId;

            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository
                .UpdateAsync(review, cancellationToken);

            _logger.LogInformation($"Review обновлён по Id: {updateDto.Id}");
        }
    }
}
