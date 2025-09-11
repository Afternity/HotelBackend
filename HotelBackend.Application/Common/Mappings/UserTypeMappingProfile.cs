using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.UserTypeViewModels;
using HotelBackend.Domain.Models;
using AutoMapper;

namespace HotelBackend.Application.Common.Mappings
{
    public class UserTypeMappingProfile : Profile
    {
        public UserTypeMappingProfile()
        {
            CreateMap<CreateUserTypeDto, UserType>();
            CreateMap<UpdateUserTypeDto, UserType>()
                 .ForMember(destination => destination.Id,
                    options => options.Ignore());
            CreateMap<UserType, UserTypeVm>();
            CreateMap<UserType, UserTypeLookupDto>();
        }
    }
}
