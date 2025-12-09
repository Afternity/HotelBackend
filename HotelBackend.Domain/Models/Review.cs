using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class Review
        : BaseModel
    {
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; } = 5;

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; } = null!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; } = null!;
    }
}
