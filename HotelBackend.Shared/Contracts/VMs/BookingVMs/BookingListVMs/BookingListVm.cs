using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs
{
    public class BookingListVm
    {
        public IList<BookingLookupDto> BookingLookups { get; set; } = [];
    }
}
