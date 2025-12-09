using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.ReservationViewModes;
using HotelBackend.Domain.Models;
using AutoMapper;

namespace HotelBackend.Application.Common.Mappings
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<CreateReservationDto, Booking>();
            CreateMap<UpdateReservationDto, Booking>()
                .ForMember(destination => destination.Id,
                    options => options.Ignore());
            CreateMap<Booking, ReservationVm>();
            CreateMap<Booking, ReservationLookupDto>();
        }
    }
}
