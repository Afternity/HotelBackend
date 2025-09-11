using HotelBackend.Application.Common.Contracts.DTOs.UserDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.UserViewModes;

namespace HotelBackend.Application.Features.InterfacesServices
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
