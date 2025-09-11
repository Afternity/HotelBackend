namespace HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs
{
    public class UpdateReservationDto
    {
        public Guid Id {  get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;

        public Guid RoomId { get; set; }
    }
}
