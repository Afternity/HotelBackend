namespace HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewLookupDTOs
{
    public class ReviewLookupDto
    {
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;

        public Guid BookingId { get; set; }
    }
}
