using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class Reservation : BaseModel
    {
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;

        public Guid RoomId { get; set; } 
        public Room? Room { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
