using HotelBackend.Domain.Models;
using AutoMapper;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    public class BookingMappingProfile 
        : Profile
    {
        public BookingMappingProfile()
        {
            // createDto
            CreateMap<CreateBookingDto, Booking>();

            // updateDto
            CreateMap<UpdateBookingDto, Booking>();

            // detailsVm
            CreateMap<Booking, BookingDetailsVm>();

            // lookupDto
            CreateMap<Booking, BookingLookupDto>();
        }
    }
}
