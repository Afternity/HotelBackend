using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(
            IBookingService reservationService)
        {
            _bookingService = reservationService;
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<ActionResult<BookingDetailsVm>> Get(
            [FromQuery] GetBookingDto getDto,
            CancellationToken cancellationToken)
        {
            var booking = await _bookingService
                .GetByIdAsync(getDto, cancellationToken);

            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpPost("create")]
        [Authorize(Roles = RoleConsts.Client,)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookingDto createDto,
            CancellationToken cancellationToken)
        {
            var bookingId = await _bookingService
                .CreateAsync(createDto, cancellationToken);

            if (bookingId == Guid.Empty)
                return BadRequest("Не удалось создать бронирование");

            return Ok(bookingId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateBookingDto updateDto,
            CancellationToken cancellationToken)
        {
            await _bookingService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeleteBookingDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _bookingService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeleteBookingDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _bookingService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<BookingListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var bookings = await _bookingService
                .GetAllAsync(cancellationToken);

            return Ok(bookings);
        }

        [HttpGet("get-user-bookings")]
        public async Task<ActionResult<UserBookingListVm>> GetAllByUser(
            [FromQuery] GetUserBookingsDto getDto,
            CancellationToken cancellationToken)
        {
            var bookings = await _bookingService
                .GetAllByUserAsync(getDto, cancellationToken);

            return Ok(bookings);
        }

        [HttpGet("get-last-user-booking")]
        public async Task<ActionResult<BookingDetailsVm>> GetLastByUser(
            [FromQuery] GetLastUserBookingDto getDto,
            CancellationToken cancellationToken)
        {
            var booking = await _bookingService
                .GetLastBookingByUserAsync(getDto, cancellationToken);

            return booking == null ? NotFound() : Ok(booking);
        }
    }
}
