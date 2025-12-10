using HotelBackend.Shared.Contracts.Enums.RoomEnums;

namespace HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs
{
    public record CreateRoomDto
    {
        public string Number { get; set; } = string.Empty;
        public RoomClass Class { get; set; } 
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } 
    }
}
