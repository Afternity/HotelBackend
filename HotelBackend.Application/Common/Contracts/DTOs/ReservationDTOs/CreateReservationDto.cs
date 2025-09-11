namespace HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs
{
    public class CreateReservationDto
    {
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
