using HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.RoomViewModels;
using HotelBackend.Domain.Models;
using AutoMapper;

namespace HotelBackend.Application.Common.Mappings
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>()
                .ForMember(destination => destination.Id,
                    options => options.Ignore());
            CreateMap<Room, RoomVm>();
            CreateMap<Room, RoomLookupDto>();
        }
    }
}
