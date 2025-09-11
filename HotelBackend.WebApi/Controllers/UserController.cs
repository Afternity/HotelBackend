using HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs;
using HotelBackend.Application.Common.Contracts.DTOs.UserDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.RoomViewModels;
using HotelBackend.Application.Common.Contracts.ViewModels.UserViewModes;
using HotelBackend.Application.Features.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("get-user/{id}")]
        public async Task<ActionResult<UserVm>> Get(
            Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started find user: {id}");

            var findDto = new FindAndDeleteUserDto()
            {
                Id = id,
            };

            var room = await _userService.GetByIdAsync(findDto, cancellationToken);

            _logger.LogInformation($"Ended find user: {nameof(findDto)}");

            return Ok(room);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started create user: {nameof(createDto)}");

            var roomId = await _userService.CreateAsync(createDto, cancellationToken);

            _logger.LogInformation($"Ended create user: {nameof(roomId)}");

            return Ok(roomId);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUserDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started update user: {nameof(updateDto)}");

            await _userService.UpdateAsync(updateDto, cancellationToken);

            _logger.LogInformation($"Ended update user: {nameof(updateDto)}");

            return NoContent();
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> Delete(
            [FromBody] FindAndDeleteUserDto deleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started delete user: {nameof(deleteDto)}");

            await _userService.DeleteAsync(deleteDto, cancellationToken);

            _logger.LogInformation($"Ended delete user: {nameof(deleteDto)}");

            return NoContent();
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<RoomListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started get All users");

            var rooms = await _userService.GetAllAsync(cancellationToken);

            _logger.LogInformation($"Ended get All users");

            return Ok(rooms);
        }
    }
}
