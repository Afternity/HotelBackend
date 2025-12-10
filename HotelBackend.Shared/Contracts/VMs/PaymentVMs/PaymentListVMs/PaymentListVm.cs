using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentListVMs
{
    public class PaymentListVm
    {
        public ICollection<PaymentLookupDto> PaymentLookups { get; set; } = [];
    }
}
