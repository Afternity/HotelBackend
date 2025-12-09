using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs;
using HotelBackend.Shared.Contracts.ViewModels.UserTypeViewModels;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IUserTypeService
    {
        Task<UserTypeVm> GetByIdAsync(
            FindAndDeleteUserTypeDto findDto,
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            CreateUserTypeDto createDto,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            UpdateUserTypeDto updateDto,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            FindAndDeleteUserTypeDto deleteDto,
            CancellationToken cancellationToken);
        Task<UserTypeListVm> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
