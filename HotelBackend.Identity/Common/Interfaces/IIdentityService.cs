using HotelBackend.Identity.Common.Models;
using HotelBackend.Shared.Contracts.Containers.IdentityContainers;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.AuthorizationDTOs;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.RegistrationDTOs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.AuthorizationVMs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.RegistrationVMs;

namespace HotelBackend.Identity.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthorizationVm> AuthorizationAsync(
            AuthorizationDto authorizationDto,
            CancellationToken cancellationToken);
        Task<RegistrationVm> RegistrationAsync(
            RegistrationDto registrationDto,
            CancellationToken cancellationToken);
        Task<JwtTokensContainer> RefreshTokenAsync(
            JwtTokensContainer jwtTokenContainer,
            CancellationToken cancellationToken);
        Task RevokeAsync(
            string userEmail,
            CancellationToken cancellationToken);
        Task RevokeAllAsync(
            CancellationToken cancellationToken);
        Task<bool> ConfirmEmailAsync(
            string userId,
            string token,
            CancellationToken cancellationToken);
        Task<bool> ResendConfirmationEmailAsync(
            string email,
            CancellationToken cancellationToken);
    }
}
