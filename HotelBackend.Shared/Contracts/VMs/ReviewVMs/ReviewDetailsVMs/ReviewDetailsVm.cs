namespace HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs
{
    public class ReviewDetailsVm
    {
        public int Rating { get; set; } 
        public string Text { get; set; } = string.Empty;

        public Guid BookingId { get; set; }
    }
}
