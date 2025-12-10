using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdareUserTypeDTOs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeDatailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IUserTypeService
    {
        Task<UserTypeDetailsVm> GetByIdAsync(
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
