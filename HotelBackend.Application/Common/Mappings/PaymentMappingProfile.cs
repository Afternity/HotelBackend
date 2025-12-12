using AutoMapper;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.UpdatePaymentDTOs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
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
