using HotelBackend.Shared.Contracts.Enums.PaymentEnums;

namespace HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs
{
    public record CreatePaymentDto
    {
        public decimal TotalAmount { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
        public PaymentStatus Status { get; set; } 

        public Guid BookingId { get; set; }
    }
}
