using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.DeleteRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController 
        : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(
            IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomDetailsVm>> Get(
            [FromQuery] GetRoomDto getDto,
            CancellationToken cancellationToken)
        {
            var room = await _roomService
                .GetByIdAsync(getDto, cancellationToken);

            return room == null ? NotFound() : Ok(room);
        }

        [HttpPost("create")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> Create(
            [FromBody] CreateRoomDto createDto,
            CancellationToken cancellationToken)
        {
            var roomId = await _roomService
                .CreateAsync(createDto, cancellationToken);

            if (roomId == Guid.Empty)
                return BadRequest("Не удалось создать комнату");

            return Ok(roomId);
        }

        [HttpPut("update")]
        [Authorize(Policy = PolicyConsts.StaffOnly)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateRoomDto updateDto,
            CancellationToken cancellationToken)
        {
            await _roomService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeleteRoomDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _roomService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        [Authorize(Policy = PolicyConsts.StaffOnly)]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeleteRoomDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _roomService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var rooms = await _roomService
                .GetAllAsync(cancellationToken);

            return Ok(rooms);
        }

        [HttpGet("get-by-rating")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingRoomListVm>> GetByRating(
            [FromQuery] GetAllByRatingRoomDto getAllDto,
            CancellationToken cancellationToken)
        {
            var rooms = await _roomService
                .GetAllByRatingAsync(getAllDto, cancellationToken);

            return Ok(rooms);
        }
    }
}
