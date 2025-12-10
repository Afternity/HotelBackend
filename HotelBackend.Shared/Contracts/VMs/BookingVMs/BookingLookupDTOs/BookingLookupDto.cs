namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs
{
    public class BookingLookupDto
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; } 
        public DateTime CheckOutDate { get; set; } 

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
