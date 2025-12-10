using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewLookupDTOs;
using Microsoft.VisualBasic;

namespace HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewListVMs
{
    public class ReviewListVm
    {
        public ICollection<ReviewLookupDto> ReviewLookups { get; set; } = [];
    }
}
