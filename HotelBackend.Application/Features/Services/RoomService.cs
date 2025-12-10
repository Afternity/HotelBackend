using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Application.Common.Exceptions;
using HotelBackend.Domain.Models;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AutoMapper;

namespace HotelBackend.Application.Features.Services
{
    public class RoomService 
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<RoomService> _logger;

        private readonly IMapper _mapper;
        //private readonly IValidator<CreateRoomDto> _createRoomDtoValidator;
        //private readonly IValidator<UpdateRoomDto> _updateRoomDtoValidator;
        //private readonly IValidator<FindAndDeleteRoomDto> _findAndDeleteRoomDtoValidator;

        //public RoomService(
        //    IRoomRepository roomRepository,
        //    ILogger<RoomService> logger,
        //    IMapper mapper,
        //    IValidator<CreateRoomDto> createRoomDtoValidator,
        //    IValidator<UpdateRoomDto> updateRoomDtoValidator,
        //    IValidator<FindAndDeleteRoomDto> findAndDeleteRoomDtoValidator)
        //{
        //    _roomRepository = roomRepository;
        //    _logger = logger;
        //    _mapper = mapper;
        //    _createRoomDtoValidator = createRoomDtoValidator;
        //    _updateRoomDtoValidator = updateRoomDtoValidator;
        //    _findAndDeleteRoomDtoValidator = findAndDeleteRoomDtoValidator;
        //}

        //public async Task<Guid> CreateAsync(
        //    CreateRoomDto createDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Create room: {nameof(createDto)}");

        //    var validationResult = await _createRoomDtoValidator
        //        .ValidateAsync(createDto, cancellationToken);

        //    if (validationResult.IsValid == false)
        //        throw new ValidationException(validationResult.Errors);

        //    var createRoom = _mapper.Map<Room>(createDto);

        //    _logger.LogInformation($"Validation and Mapping are successful");

        //    var roomId = await _roomRepository.CreateAsync(createRoom, cancellationToken);

        //    _logger.LogInformation($"Room is created, createRoom.Id = {roomId}");

        //    return roomId;
        //}

        //public async Task DeleteAsync(
        //    FindAndDeleteRoomDto deleteDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Delete room: {nameof(deleteDto)}");

        //    var validationResult = await _findAndDeleteRoomDtoValidator
        //        .ValidateAsync(deleteDto, cancellationToken);

        //    if (validationResult.IsValid == false)
        //        throw new ValidationException(validationResult.Errors);

        //    _logger.LogInformation($"Validation is successful");

        //    var deleteRoom = await _roomRepository
        //        .GetByIdAsync(deleteDto.Id, cancellationToken);

        //    if (deleteRoom == null)
        //        throw new NotFoundException(nameof(deleteRoom), deleteDto.Id);

        //    await _roomRepository.DeleteAsync(deleteRoom, cancellationToken);

        //    _logger.LogInformation($"Room is deleted, deleteRoom.Id = {deleteRoom.Id}");
        //}

        //public async Task<RoomListVm> GetAllAsync(
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Get All rooms");

        //    var rooms = await _roomRepository
        //        .GetAllAsync(cancellationToken);

        //    _logger.LogInformation($"Rooms: {nameof(rooms)}");

        //    return new RoomListVm()
        //    {
        //        Rooms = _mapper.Map<IList<RoomLookupDto>>(rooms)
        //    };
        //}

        //public async Task<RoomVm> GetByIdAsync(
        //    FindAndDeleteRoomDto findDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Get room: {nameof(findDto)}");

        //    var validationResult = await _findAndDeleteRoomDtoValidator
        //        .ValidateAsync(findDto);

        //    if (validationResult.IsValid == false)
        //        throw new ValidationException(validationResult.Errors);

        //    _logger.LogInformation($"Validation is successful");

        //    var room = await _roomRepository
        //        .GetByIdAsync(findDto.Id, cancellationToken);

        //    if (room == null)
        //        throw new NotFoundException(nameof(room), findDto.Id);

        //    _logger.LogInformation($"Room: {room.Id}");

        //    return _mapper.Map<RoomVm>(room);
        //}

        //public async Task UpdateAsync(
        //    UpdateRoomDto updateDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Update room: {nameof(updateDto)}");

        //    var validationResult = await _updateRoomDtoValidator
        //        .ValidateAsync(updateDto);

        //    if (validationResult.IsValid == false)
        //        throw new ValidationException(validationResult.Errors);

        //    _logger.LogInformation($"Validation is successful");

        //    var existingRoom = await _roomRepository
        //        .GetByIdAsync(updateDto.Id, cancellationToken);

        //    if (existingRoom == null)
        //        throw new NotFoundException(nameof(existingRoom), updateDto.Id);

        //    var updateRoom = _mapper.Map(updateDto, existingRoom);

        //    _logger.LogInformation($"Mapping is successful");

        //    await _roomRepository.UpdateAsync(updateRoom, cancellationToken);

        //    _logger.LogInformation($"Room is updated, updateRoom.Id = {updateRoom.Id}");
        //}
    }
}
