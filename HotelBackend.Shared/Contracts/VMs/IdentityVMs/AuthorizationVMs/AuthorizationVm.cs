using HotelBackend.Shared.Contracts.Containers.IdentityContainers;

namespace HotelBackend.Shared.Contracts.VMs.IdentityVMs.AuthorizationVMs
{
    public class AuthorizationVm
    {
        public JwtTokensContainer Tokens { get; set; }
    }
}
