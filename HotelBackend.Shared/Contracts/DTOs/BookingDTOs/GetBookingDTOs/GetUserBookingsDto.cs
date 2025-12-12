namespace HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs
{
    public record GetUserBookingsDto
    {
        public Guid UserId { get; set; }
    }
}
