using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeListVMs
{
    public class UserTypeListVm
    {
        public IList<UserTypeLookupDto> UserTypeLookups { get; set; } = [];
    }
}
