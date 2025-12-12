using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.CreateUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.GetUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;
using HotelBackend.Shared.Contracts.VMs.UserViewModes.UserListVMs;
using HotelBackend.Shared.Contracts.VMs.UserVMs.UserDetailsVMs;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController 
        : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<UserDetailsVm>> Get(
            [FromQuery] GetUserDto getDto,
            CancellationToken cancellationToken)
        {
            var user = await _userService
                .GetByIdAsync(getDto, cancellationToken);

            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserDto createDto,
            CancellationToken cancellationToken)
        {
            var userId = await _userService
                .CreateAsync(createDto, cancellationToken);

            if (userId == Guid.Empty)
                return BadRequest("Не удалось создать пользователя");

            return Ok(userId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUserDto updateDto,
            CancellationToken cancellationToken)
        {
            await _userService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeleteUserDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _userService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeleteUserDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _userService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<UserListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var users = await _userService
                .GetAllAsync(cancellationToken);

            return Ok(users);
        }

        [HttpGet("get-all-by-booking")]
        public async Task<ActionResult<UserListVm>> GetAllByBooking(
            CancellationToken cancellationToken)
        {
            var users = await _userService
                .GetAllByBookingAsync(cancellationToken);

            return Ok(users);
        }
    }
}
