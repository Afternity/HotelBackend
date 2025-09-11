using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.ReservationViewModes;
using HotelBackend.Application.Features.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(
            IReservationService reservationService,
            ILogger<ReservationController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpGet("get-reservation/{id}")]
        public async Task<ActionResult<ReservationVm>> Get(
            Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started find reservation: {id}");

            var findDto = new FindAndDeleteReservationDto()
            {
                Id = id,
            };

            var room = await _reservationService.GetByIdAsync(findDto, cancellationToken);

            _logger.LogInformation($"Ended find reservation: {nameof(findDto)}");

            return Ok(room);
        }

        [HttpPost("create-reservation")]
        public async Task<IActionResult> Create(
            [FromBody] CreateReservationDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started create reservation: {nameof(createDto)}");

            var roomId = await _reservationService.CreateAsync(createDto, cancellationToken);

            _logger.LogInformation($"Ended create reservation: {nameof(roomId)}");

            return Ok(roomId);
        }

        [HttpPut("update-reservation")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateReservationDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started update reservation: {nameof(updateDto)}");

            await _reservationService.UpdateAsync(updateDto, cancellationToken);

            _logger.LogInformation($"Ended update reservation: {nameof(updateDto)}");

            return NoContent();
        }

        [HttpDelete("delete-room")]
        public async Task<IActionResult> Delete(
            [FromBody] FindAndDeleteReservationDto deleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started delete reservation: {nameof(deleteDto)}");

            await _reservationService.DeleteAsync(deleteDto, cancellationToken);

            _logger.LogInformation($"Ended delete reservation: {nameof(deleteDto)}");

            return NoContent();
        }

        [HttpGet("get-rooms")]
        public async Task<ActionResult<ReservationListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started get All reservation");

            var rooms = await _reservationService.GetAllAsync(cancellationToken);

            _logger.LogInformation($"Ended get All reservation");

            return Ok(rooms);
        }

        [HttpGet("get-my-rooms/{id}")]
        public async Task<ActionResult<ReservationListVm>> GetMyAll(
            Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started get All reservation");

            var findDto = new FindAndDeleteReservationDto()
            {
                Id = id
            };
            var rooms = await _reservationService.GetMyAllAsync(findDto, cancellationToken);

            _logger.LogInformation($"Ended get All reservation");

            return Ok(rooms);
        }
    }
}
