using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IRoomService
    {
        Task<RoomDetailsVm> GetByIdAsync(
            FindAndDeleteRoomDto findDto,
            CancellationToken cancellationToken);
        Task<RoomListVm> GetAllAsync(
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            CreateRoomDto createDto,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            UpdateRoomDto updateDto,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            FindAndDeleteRoomDto deleteDto,
            CancellationToken cancellationToken);
    }
}
