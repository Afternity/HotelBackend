using HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.RoomViewModels;

namespace HotelBackend.Application.Features.InterfacesServices
{
    public interface IRoomService
    {
        Task<RoomVm> GetByIdAsync(
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
