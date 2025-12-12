using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.DeleteRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IRoomService
    {
        /// <summary>
        /// Поиск Room по Id.
        /// GetRoomDto для единообразия.
        /// </summary>
        Task<RoomDetailsVm?> GetByIdAsync(
            GetRoomDto getDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создание новой Room.
        /// </summary>
        Task<Guid> CreateAsync(
            CreateRoomDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное обновление Room.
        /// </summary>
        Task UpdateAsync(
            UpdateRoomDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление Room.
        /// HardDeleteRoomDto для единоодразия.
        /// </summary>
        Task HardDeleteAsync(
            HardDeleteRoomDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление Room.
        /// SoftDeleteRoomDto для единообразия.
        /// </summary>
        Task SoftDeleteAsync(
            SoftDeleteRoomDto softDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех Room, где IsDeleted = false.
        /// </summary>
        Task<RoomListVm> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всез Room с определенным Rating, где IsDeleted = false. 
        /// </summary>
        Task<RatingRoomListVm> GetAllByRatingAsync(
            GetAllByRatingRoomDto getAllDto,
            CancellationToken cancellationToken);
    }
}
