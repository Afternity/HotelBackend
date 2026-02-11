using HotelBackend.Identity.Common.Interfaces;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Shared.Contracts.Containers.IdentityContainers;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.AuthorizationDTOs;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.RegistrationDTOs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.AuthorizationVMs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.RegistrationVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController
        : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(
            IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorizationVm>> Authenticate(
            [FromBody] AuthorizationDto authorizationDto,
            CancellationToken cancellationToken)
        {
            var result = await _identityService
                .AuthorizationAsync(authorizationDto, cancellationToken);

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationVm>> Registration(
            [FromBody] RegistrationDto registrationDto,
            CancellationToken cancellationToken)
        {
            var result = await _identityService
                .RegistrationAsync(registrationDto, cancellationToken);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [Authorize(Policy = PolicyConsts.All)]
        public async Task<ActionResult<TokenModel>> RefreshToken(
            JwtTokensContainer tokenModel,
            CancellationToken cancellationToken)
        {
            var result = await _identityService
                 .RefreshTokenAsync(tokenModel, cancellationToken);

            return Ok(result);
        }

        [HttpPost("revoke/{email}")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> Revoke(
            string email,
            CancellationToken cancellationToken)
        {
            await _identityService
                 .RevokeAsync(email, cancellationToken);

            return Ok();
        }

        [HttpPost("revoke-all")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> RevokeAll(
            CancellationToken cancellationToken)
        {
            await _identityService.RevokeAllAsync(cancellationToken);

            return Ok();
        }

        [HttpPost("resend-confirmation-email")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ResendConfirmationEmailAsync(
            string email,
            CancellationToken cancellationToken)
        {
            bool flag = await _identityService
                .ResendConfirmationEmailAsync(email, cancellationToken);

            return Ok(flag);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ConfirmEmailAsync(
           [FromQuery] string userId,
           [FromQuery] string token,
           CancellationToken cancellationToken)
        {
            bool flag = await _identityService
                .ConfirmEmailAsync(userId, token, cancellationToken);

            return Ok(flag);
        }
    }
}
