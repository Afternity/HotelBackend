using HotelBackend.Domain.Models;
using AutoMapper;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    /// <summary>
    /// Есть большая оговорка. Mapper работает только на VMs.
    /// </summary>
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            // createDto
            CreateMap<CreateRoomDto, Room>();

            // updateDto
            CreateMap<UpdateRoomDto, Room>();
                
            // detailsVm
            CreateMap<Room, RoomDetailsVm>();

            // lookupDto
            CreateMap<Room, RoomLookupDto>();
            CreateMap<Room, RatingRoomLookupDto>();
        }
    }
}
