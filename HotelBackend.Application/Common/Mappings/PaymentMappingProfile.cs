using AutoMapper;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.UpdatePaymentDTOs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    /// <summary>
    /// Есть большая оговорка. Mapper работает только на VMs.
    /// </summary>
    public class PaymentMappingProfile
        : Profile
    {
        public PaymentMappingProfile()
        {
            // createDto
            CreateMap<CreatePaymentDto, Payment>();

            // updateDto 
            CreateMap<UpdatePaymentDto, Payment>();

            // detailsVm
            CreateMap<Payment, PaymentDetailsVm>();

            // lookupDto
            CreateMap<Payment, PaymentLookupDto>();
            CreateMap<Payment, UserPaymentLookupDto>();
        }
    }
}
