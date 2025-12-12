using AutoMapper;
using FluentValidation;
using HotelBackend.Domain.Enums.RoomEnums;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.DeleteRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomLookupDTOs;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class RoomService
        : IRoomService
    {
        // БД contracts
        private readonly IRoomRepository _roomRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;
        // CUD validators
        private readonly IValidator<CreateRoomDto> _createRoomDtoValidator;
        private readonly IValidator<UpdateRoomDto> _updateRoomDtoValidator;
        private readonly IValidator<HardDeleteRoomDto> _hardDeleteRoomDtoValidator;
        private readonly IValidator<SoftDeleteRoomDto> _softDeleteRoomDtoValidator;
        // R validators
        private readonly IValidator<GetRoomDto> _getRoomDtoValidator;
        private readonly IValidator<GetAllByRatingRoomDto> _getAllByRatingRoomDtoValidator;

        public RoomService(
            IRoomRepository roomRepository,
            IMapper mapper,
            ILogger<RoomService> logger,
            IValidator<CreateRoomDto> createRoomDtoValidator,
            IValidator<UpdateRoomDto> updateRoomDtoValidator,
            IValidator<HardDeleteRoomDto> hardDeleteRoomDtoValidator,
            IValidator<SoftDeleteRoomDto> softDeleteRoomDtoValidator,
            IValidator<GetRoomDto> getRoomDtoValidator,
            IValidator<GetAllByRatingRoomDto> getAllByRatingRoomDtoValidator)
        {
            // БД contracts
            _roomRepository = roomRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createRoomDtoValidator = createRoomDtoValidator;
            _updateRoomDtoValidator = updateRoomDtoValidator;
            _hardDeleteRoomDtoValidator = hardDeleteRoomDtoValidator;
            _softDeleteRoomDtoValidator = softDeleteRoomDtoValidator;
            // R validators
            _getRoomDtoValidator = getRoomDtoValidator;
            _getAllByRatingRoomDtoValidator = getAllByRatingRoomDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateRoomDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания Room.");

            var validation = await _createRoomDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var newRoom = new Room()
            {
                Id = Guid.NewGuid(),

                Number = createDto.Number,
                Class = (RoomClass)createDto.Class,
                Description = createDto.Description,
                PricePerNight = createDto.PricePerNight,

                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _roomRepository
                .CreateAsync(newRoom, cancellationToken);

            _logger.LogInformation($"Room создана с Id: {newRoom.Id}");

            return newRoom.Id;
        }

        public async Task<RoomListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения Rooms, где IsDeleted == false");

            var rooms = await _roomRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("Rooms, где IsDeleted == false, получены");

            return new RoomListVm()
            {
                RoomLookups = _mapper.Map<IList<RoomLookupDto>>(rooms)
            };
        }

        public async Task<RatingRoomListVm> GetAllByRatingAsync(
            GetAllByRatingRoomDto getAllDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Rooms с рейтингом: {getAllDto.Rating}");

            var validation = await _getAllByRatingRoomDtoValidator
                .ValidateAsync(getAllDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var rooms = await _roomRepository
                .GetAllByRatingAsync(getAllDto.Rating, cancellationToken);

            _logger.LogInformation($"Rooms с рейтингом {getAllDto.Rating} получены");

            return new RatingRoomListVm()
            {
                RatingRoomLookups = _mapper.Map<IList<RatingRoomLookupDto>>(rooms)
            };
        }

        public async Task<RoomDetailsVm?> GetByIdAsync(
            GetRoomDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения Room по Id: {getDto.Id}");

            var validation = await _getRoomDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"Room найден по Id: {getDto.Id}");

            return _mapper.Map<RoomDetailsVm>(room);
        }

        public async Task HardDeleteAsync(
            HardDeleteRoomDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления Room по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeleteRoomDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            await _roomRepository
                .HardDeleteAsync(room, cancellationToken);

            _logger.LogInformation($"Room полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeleteRoomDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления Room по Id: {softDeleteDto.Id}");

            var validation = await _softDeleteRoomDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {softDeleteDto.Id}");
                return;
            }

            room.DeletedAt = DateTime.UtcNow;
            room.IsDeleted = true;

            await _roomRepository
                .SoftDeleteAsync(room, cancellationToken);

            _logger.LogInformation($"Room мягко удалён по Id: {softDeleteDto.Id}");
        }

        public async Task UpdateAsync(
            UpdateRoomDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало обновления Room по Id: {updateDto.Id}");

            var validation = await _updateRoomDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var room = await _roomRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning($"Room не найден по Id: {updateDto.Id}");
                return;
            }

            room.Number = updateDto.Number;
            room.Class = (RoomClass)updateDto.Class;
            room.Description = updateDto.Description;
            room.PricePerNight = updateDto.PricePerNight;
            room.UpdatedAt = DateTime.UtcNow;

            await _roomRepository
                .UpdateAsync(room, cancellationToken);

            _logger.LogInformation($"Room обновлён по Id: {updateDto.Id}");
        }
    }
}
