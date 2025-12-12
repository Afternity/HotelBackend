using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs
{
    public class RatingRoomListVm
    {
        public ICollection<RatingRoomLookupDto> RatingRoomLookups { get; set; } = [];
    }
}
