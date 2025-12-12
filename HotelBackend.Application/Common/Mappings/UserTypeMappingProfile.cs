using AutoMapper;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdateUserTypeDTOs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeDatailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    /// <summary>
    /// Есть большая оговорка. Mapper работает только на VMs.
    /// </summary>
    public class UserTypeMappingProfile : Profile
    {
        public UserTypeMappingProfile()
        {
            // createDto
            CreateMap<CreateUserTypeDto, UserType>();

            // updateDto
            CreateMap<UpdateUserTypeDto, UserType>();

            // detailsVm
            CreateMap<UserType, UserTypeDetailsVm>();
            
            // lookupDto
            CreateMap<UserType, UserTypeLookupDto>();
        }
    }
}
