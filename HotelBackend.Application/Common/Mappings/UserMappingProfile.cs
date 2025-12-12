using AutoMapper;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.CreateUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;
using HotelBackend.Shared.Contracts.VMs.UserVMs.UserDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // createDto
            CreateMap<CreateUserDto, User>();

            // updateDto
            CreateMap<UpdateUserDto, User>();

            // detailsVm
            CreateMap<User, UserDetailsVm>();
            
            // lookupDto
            CreateMap<User, UserLookupDto>();
            CreateMap<User, BookingUserLookupDto>();
        }
    }
}
