namespace HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs
{
    public class CreateRoomDto
    {
        public string Number { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;
    }
}
