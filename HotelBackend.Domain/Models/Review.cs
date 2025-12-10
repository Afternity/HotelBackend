using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class Review
        : BaseModel
    {
        public int Rating { get; set; } = 5;
        public string Text { get; set; } = string.Empty;

        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; } = null!;
    }
}
