namespace HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs
{
    public record GetLastUserBookingDto
    {
        public Guid UserId { get; set; }
    }
}
