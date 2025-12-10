namespace HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs
{
    public class UserPaymentLookupDto
    {
        public ICollection<PaymentLookupDto> UserPaymentLookups { get; set; } = [];
    }
}
