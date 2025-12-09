using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class Booking 
        : BaseModel
    {
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; } = null!;
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }  
        public Guid? PaymentId { get; set; }
        public virtual Payment? Payment { get; set; } 

        public virtual ICollection<Review> Reviews { get; set; } = [];

    }
}
