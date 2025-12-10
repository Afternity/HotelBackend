using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomLookupDTOs;

namespace HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs
{
    public class RoomListVm
    {
        public IList<RoomLookupDto> RoomLookups { get; set; } = [];
    }
}
