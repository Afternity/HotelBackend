
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        //private readonly IRoomService _roomService;
        //private readonly ILogger<RoomController> _logger;

        //public RoomController(
        //    IRoomService roomService,
        //    ILogger<RoomController> logger)
        //{
        //    _roomService = roomService;
        //    _logger = logger;
        //}

        //[HttpGet("get-room/{id}")]
        //public async Task<ActionResult<RoomVm>> Get(
        //    Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started find room: {id}");

        //    var findDto = new FindAndDeleteRoomDto()
        //    {
        //        Id = id,
        //    };
            
        //    var room =  await _roomService.GetByIdAsync(findDto, cancellationToken);

        //    _logger.LogInformation($"Ended find room: {nameof(findDto)}");

        //    return Ok(room);
        //}

        //[HttpPost("create-room")]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateRoomDto createDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started create room: {nameof(createDto)}");

        //    var roomId = await _roomService.CreateAsync(createDto, cancellationToken);

        //    _logger.LogInformation($"Ended create room: {nameof(roomId)}");

        //    return Ok(roomId);
        //}

        //[HttpPut("update-room")]
        //public async Task<IActionResult> Update(
        //    [FromBody] UpdateRoomDto updateDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started update room: {nameof(updateDto)}");

        //    await _roomService.UpdateAsync(updateDto, cancellationToken);

        //    _logger.LogInformation($"Ended update room: {nameof(updateDto)}");

        //    return NoContent();
        //}

        //[HttpDelete("delete-room")]
        //public async Task<IActionResult> Delete(
        //    [FromBody] FindAndDeleteRoomDto deleteDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started delete room: {nameof(deleteDto)}");

        //    await _roomService.DeleteAsync(deleteDto, cancellationToken);

        //    _logger.LogInformation($"Ended delete room: {nameof(deleteDto)}");

        //    return NoContent();
        //}

        //[HttpGet("get-rooms")]
        //public async Task<ActionResult<RoomListVm>> GetAll(
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started get All rooms");

        //    var rooms = await _roomService.GetAllAsync(cancellationToken);

        //    _logger.LogInformation($"Ended get All rooms");

        //    return Ok(rooms);
        //}
    }
}
