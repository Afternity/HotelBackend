using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentListVMs
{
    public class UserPaymentListVm
    {
        public ICollection<UserPaymentLookupDto> UserPaymentLookups { get; set; } = [];
    }
}
