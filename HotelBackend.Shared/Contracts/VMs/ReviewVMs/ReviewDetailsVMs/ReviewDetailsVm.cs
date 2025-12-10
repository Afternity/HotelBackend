namespace HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs
{
    public class ReviewDetailsVm
    {
        public int Rating { get; set; } 
        public string Text { get; set; } = string.Empty;

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookingId { get; set; }
    }
}
