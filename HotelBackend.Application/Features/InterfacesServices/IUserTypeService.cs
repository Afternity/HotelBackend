using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.UserTypeViewModels;

namespace HotelBackend.Application.Features.InterfacesServices
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
