namespace HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs
{
    public record CreateBookingDto
    {
        public DateTime CheckInDate { get; set; } 
        public DateTime CheckOutDate { get; set; } 

        public Guid PaymentId { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
