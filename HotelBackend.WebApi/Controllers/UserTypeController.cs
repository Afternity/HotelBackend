using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.DeleteUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdateUserTypeDTOs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeDatailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeListVMs;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController 
        : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(
            IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<UserTypeDetailsVm>> Get(
            [FromQuery] GetUserTypeDto getDto,
            CancellationToken cancellationToken)
        {
            var userType = await _userTypeService
                .GetByIdAsync(getDto, cancellationToken);

            return userType == null ? NotFound() : Ok(userType);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserTypeDto createDto,
            CancellationToken cancellationToken)
        {
            var userTypeId = await _userTypeService
                .CreateAsync(createDto, cancellationToken);

            if (userTypeId == Guid.Empty)
                return BadRequest("Не удалось создать тип пользователя");

            return Ok(userTypeId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUserTypeDto updateDto,
            CancellationToken cancellationToken)
        {
            await _userTypeService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeleteUserTypeDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _userTypeService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeleteUserTypeDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _userTypeService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<UserTypeListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var userTypes = await _userTypeService
                .GetAllAsync(cancellationToken);

            return Ok(userTypes);
        }
    }
}
