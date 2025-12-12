using HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.UserViewModes.UserListVMs
{
    public class UserListVm
    {
        public IList<UserLookupDto> UserLookups { get; set; } = [];
    }
}
