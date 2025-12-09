using AutoMapper;
using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.ReservationViewModes;
using HotelBackend.Application.Common.Exceptions;
using HotelBackend.Application.Features.InterfacesServices;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<ReservationService> _logger;

        private readonly IMapper _mapper;
        private readonly IValidator<CreateReservationDto> _createReservationDtoValidato;
        private readonly IValidator<UpdateReservationDto> _updateReservationDtoValidato;
        private readonly IValidator<FindAndDeleteReservationDto> _findAndDeleteReservationDtoValidato;

        public ReservationService(
            IReservationRepository reservationRepository,
            ILogger<ReservationService> logger,
            IMapper mapper,
            IValidator<CreateReservationDto> createReservationDtoValidato,
            IValidator<UpdateReservationDto> updateReservationDtoValidato,
            IValidator<FindAndDeleteReservationDto> findAndDeleteReservationDtoValidato)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
            _mapper = mapper;
            _createReservationDtoValidato = createReservationDtoValidato;
            _updateReservationDtoValidato = updateReservationDtoValidato;
            _findAndDeleteReservationDtoValidato = findAndDeleteReservationDtoValidato;
        }

        public async Task<bool> CheckDatesAsync(
            Guid reservationId,
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Check Dates reservation:" +
                $" {nameof(checkInDate)}, {nameof(checkOutDate)}");
            
            var reservations = await _reservationRepository
                .CheckDatesAsync(
                    reservationId,
                    roomId,
                    checkInDate, 
                    checkOutDate,
                    cancellationToken);

            if (reservations.Count == 0)
                return false;

            _logger.LogInformation($"Reservations not null");

            return true;
        }

        public async Task<bool> CheckDatesAsync(
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Check Dates reservation:" +
                $" {nameof(checkInDate)}, {nameof(checkOutDate)}");

            var reservations = await _reservationRepository
                .CheckDatesAsync(
                    roomId,
                    checkInDate,
                    checkOutDate,
                    cancellationToken);

            if (reservations.Count == 0)
                return false;

            _logger.LogInformation($"Reservations not null");

            return true;
        }

        public async Task<Guid> CreateAsync(
            CreateReservationDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create reservation: {nameof(createDto)}");

            var validationResult = await _createReservationDtoValidato
                .ValidateAsync(createDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            bool flag = await CheckDatesAsync(
                createDto.RoomId,
                createDto.CheckInDate, 
                createDto.CheckOutDate,
                cancellationToken);

            if (flag)
                throw new IsCheckDateException(nameof(createDto), flag);

            var user =  await _reservationRepository.GetGuestsById(createDto.UserId);

            if (user == null)
                throw new NotFoundException(nameof(user), createDto.UserId);

            var createReservation = _mapper.Map<Booking>(createDto);

            createReservation.GuestName = user.Name;
            createReservation.GuestEmail = user.Email;

            _logger.LogInformation($"Validation and Mapping are successful");

            var reservationId = await _reservationRepository.CreateAsync(createReservation, cancellationToken);

            _logger.LogInformation($"Reservation is created, createReservation.Id = {reservationId}");

            return reservationId;
        }

        public async Task DeleteAsync(
            FindAndDeleteReservationDto deleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete reservation: {nameof(deleteDto)}");

            var validationResult = await _findAndDeleteReservationDtoValidato
                .ValidateAsync(deleteDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            var deleteReservation = await _reservationRepository
                .GetByIdAsync(deleteDto.Id, cancellationToken);

            if (deleteReservation == null)
                throw new NotFoundException(nameof(deleteReservation), deleteDto.Id);

            await _reservationRepository.DeleteAsync(deleteReservation, cancellationToken);

            _logger.LogInformation($"Reservation is deleted, deleteRoom.Id = {deleteReservation.Id}");
        }

        public async Task<ReservationListVm> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get All reservations");

            var reservations = await _reservationRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation($"Reservations: {nameof(reservations)}");

            return new ReservationListVm()
            {
                Reservations = _mapper.Map<IList<ReservationLookupDto>>(reservations)
            };
        }

        public async Task<ReservationVm> GetByIdAsync(
            FindAndDeleteReservationDto findDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get reservation: {nameof(findDto)}");

            var validationResult = await _findAndDeleteReservationDtoValidato
                .ValidateAsync(findDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            var reservation = await _reservationRepository
                .GetByIdAsync(findDto.Id, cancellationToken);

            if (reservation == null)
                throw new NotFoundException(nameof(reservation), findDto.Id);

            _logger.LogInformation($"Room: {reservation.Id}");

            return _mapper.Map<ReservationVm>(reservation);
        }

        public async Task<ReservationMyListVm> GetMyAllAsync(
            FindAndDeleteReservationDto findDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get All MY reservations");

            var validationResult = await _findAndDeleteReservationDtoValidato
               .ValidateAsync(findDto);

            _logger.LogInformation($"Validation is successful");

            var myReservations = await _reservationRepository
                .GetMyAllAsync(findDto.Id, cancellationToken);

            _logger.LogInformation($"Reservations: {nameof(myReservations)}");

            return new ReservationMyListVm()
            {
                MyReservations = _mapper.Map<IList<ReservationLookupDto>>(myReservations)
            };
        }

        public async Task UpdateAsync(
            UpdateReservationDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update reservation: {nameof(updateDto)}");

            var validationResult = await _updateReservationDtoValidato
                .ValidateAsync(updateDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            bool flag = await CheckDatesAsync(
                updateDto.Id,
                updateDto.RoomId,
                updateDto.CheckInDate,
                updateDto.CheckOutDate,
                cancellationToken);

            if (flag)
                throw new IsCheckDateException(nameof(updateDto), flag);

            var existingRoom = await _reservationRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (existingRoom == null)
                throw new NotFoundException(nameof(existingRoom), updateDto.Id);

            var updaterReservation = _mapper.Map(updateDto, existingRoom);

            _logger.LogInformation($"Validation and Mapping is successful");

            await _reservationRepository.UpdateAsync(updaterReservation, cancellationToken);

            _logger.LogInformation($"Reservation is updated, deleteRoom.Id = {updaterReservation.Id}");
        }
    }
}
