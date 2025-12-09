using HotelBackend.Shared.Contracts.DTOs.UserDTOs;
using HotelBackend.Shared.Contracts.ViewModels.UserViewModes;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IUserService
    {
        Task<UserVm> GetByIdAsync(
          FindAndDeleteUserDto findDto,
          CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            CreateUserDto createDto,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            UpdateUserDto updateDto,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            FindAndDeleteUserDto deleteDto,
            CancellationToken cancellationToken);
        Task<UserListVm> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
