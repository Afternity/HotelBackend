using HotelBackend.Shared.Contracts.Enums.PaymentEnums;

namespace HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs
{
    public class UserPaymentLookupDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }

        public Guid BookingId { get; set; }
    }
}
