using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs
{
    public class UserBookingListVm
    {
        public IList<BookingLookupDto> UserBookingLookups { get; set; } = [];
    }
}
