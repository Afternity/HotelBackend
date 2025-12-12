using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs
{
    public class UserBookingListVm
    {
        public IList<UserBookingLookupDto> UserBookingLookups { get; set; } = [];
    }
}
