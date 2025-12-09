namespace HotelBackend.Shared.Contracts.ViewModels.ReservationViewModes
{
    public class ReservationLookupDto
    {
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
