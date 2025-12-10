using HotelBackend.Shared.Contracts.DTOs.UserDTOs.CreateUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.GetUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;
using HotelBackend.Shared.Contracts.VMs.UserViewModes.UserDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserViewModes.UserListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IUserService
    {
        /// <summary>
        /// Поиск User по Id.
        /// GetUserDto для единообразия.
        /// </summary>
        Task<UserDetailsVm?> GetByIdAsync(
          GetUserDto getDto,
          CancellationToken cancellationToken);

        /// <summary>
        /// Создание нового User.
        /// </summary>
        Task<Guid> CreateAsync(
            CreateUserDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное обнавление User.
        /// </summary>
        Task UpdateAsync(
            UpdateUserDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление.
        /// HardDeleteUserDto для единообразия.
        /// </summary>
        Task HardDeleteAsync(
            HardDeleteUserDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление.
        /// SoftDeleteUserDto для единообразия.
        /// </summary>
        Task SoftDeleteAsync(
            SoftDeleteUserDto softDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех User, где IsDeleted = false.
        /// </summary>
        Task<UserListVm> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех Room у кого есть бронирование и где IsDeleted = false.
        /// </summary>
        Task<UserListVm> GetAllByBookingAsync(
            CancellationToken cancellationToken);
    }
}
