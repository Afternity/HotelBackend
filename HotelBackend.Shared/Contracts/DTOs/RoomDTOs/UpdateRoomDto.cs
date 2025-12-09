namespace HotelBackend.Shared.Contracts.DTOs.RoomDTOs
{
    public class UpdateRoomDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;
    }
}
