using HotelBackend.Domain.BaseModels;
using HotelBackend.Domain.Emums.PaymentEnums;

namespace HotelBackend.Domain.Models
{
    public class Payment 
        : BaseModel
    {
        public decimal TotalAmount { get; set; } = decimal.Zero;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Card;
        public PaymentStatus Status { get; set; } = PaymentStatus.Cancelled;

        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; } = null!;
    }
}
