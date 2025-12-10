using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewListVMs
{
    public class RatingAndRoomReviewListVm
    {
        public ICollection<ReviewLookupDto> RatingAndRoomReviewLookups { get; set; } = [];
    }
}
