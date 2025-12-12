namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs
{
    public class UserBookingLookupDto
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
