using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdareUserTypeDTOs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeDatailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IUserTypeService
    {
        /// <summary>
        /// Поиск UserType по Id.
        /// GetUserTypeDto для единообразия.
        /// </summary>
        Task<UserTypeDetailsVm?> GetByIdAsync(
            GetUserTypeDto getDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создание нового UserType.
        /// </summary>
        Task<Guid> CreateAsync(
            CreateUserTypeDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное обновление UserType.
        /// </summary>
        Task UpdateAsync(
            UpdateUserTypeDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление UserType.
        /// </summary>
        Task HardDeleteAsync(
            HardDeleteUserDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление UserType.
        /// </summary>
        Task SoftDeleteAsync(
            SoftDeleteUserDto softDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех RoomType у кого есть бронирование и где IsDeleted = false.
        /// </summary>
        Task<UserTypeListVm> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
