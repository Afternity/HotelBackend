using HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.UserViewModes.UserListVMs
{
    public class BookingUserListVm
    {
        public ICollection<BookingUserLookupDto> BookingUserLookups { get; set; } = [];
    }
}
