using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController : ControllerBase
    {
        //private readonly IUserTypeService _userTypeService;
        //private readonly ILogger<UserTypeController> _logger;

        //public UserTypeController(
        //    IUserTypeService userTypeService,
        //    ILogger<UserTypeController> logger)
        //{
        //    _userTypeService = userTypeService;
        //    _logger = logger;
        //}

        //[HttpGet("get-user-type/{id}")]
        //public async Task<ActionResult<UserTypeVm>> Get(
        //    Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started find user type: {id}");

        //    var findDto = new FindAndDeleteUserTypeDto { Id = id };
        //    var userType = await _userTypeService.GetByIdAsync(findDto, cancellationToken);

        //    _logger.LogInformation($"Ended find user type: {id}");
        //    return Ok(userType);
        //}

        //[HttpPost("create-user-type")]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateUserTypeDto createDto, 
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started create user type: {nameof(createDto)}");

        //    var userTypeId = await _userTypeService.CreateAsync(createDto, cancellationToken);

        //    _logger.LogInformation($"Ended create user type: {userTypeId}");
        //    return Ok(userTypeId);
        //}

        //[HttpPut("update-user-type")]
        //public async Task<IActionResult> Update(
        //    [FromBody] UpdateUserTypeDto updateDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started update user type: {nameof(updateDto)}");

        //    await _userTypeService.UpdateAsync(updateDto, cancellationToken);

        //    _logger.LogInformation($"Ended update user type: {updateDto.Id}");
        //    return NoContent();
        //}

        //[HttpDelete("delete-user-type")]
        //public async Task<IActionResult> Delete(
        //    [FromBody] FindAndDeleteUserTypeDto deleteDto,
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation($"Started delete user type: {nameof(deleteDto)}");

        //    await _userTypeService.DeleteAsync(deleteDto, cancellationToken);

        //    _logger.LogInformation($"Ended delete user type: {deleteDto.Id}");
        //    return NoContent();
        //}

        //[HttpGet("get-user-types")]
        //public async Task<ActionResult<UserTypeListVm>> GetAll(
        //    CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Started get all user types");

        //    var userTypes = await _userTypeService.GetAllAsync(cancellationToken);

        //    _logger.LogInformation("Ended get all user types");
        //    return Ok(userTypes);
        //}
    }
}
