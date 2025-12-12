using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.DeleteUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdateUserTypeDTOs;
using HotelBackend.Shared.Contracts.VMs.UserTypeViewModels.UserTypeDatailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeListVMs;
using HotelBackend.Shared.Contracts.VMs.UserTypeVMs.UserTypeLookupDTOs;


namespace HotelBackend.Application.Features.Services
{
    public class UserTypeService
        : IUserTypeService
    {
        // БД contracts
        private readonly IUserTypeRepository _userTypeRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<UserTypeService> _logger;
        // CUD validators
        private readonly IValidator<CreateUserTypeDto> _createUserTypeDtoValidator;
        private readonly IValidator<UpdateUserTypeDto> _updateUserTypeDtoValidator;
        private readonly IValidator<HardDeleteUserTypeDto> _hardDeleteUserTypeDtoValidator;
        private readonly IValidator<SoftDeleteUserTypeDto> _softDeleteUserTypeDtoValidator;
        // R validators
        private readonly IValidator<GetUserTypeDto> _getUserTypeDtoValidator;

        public UserTypeService(
            IUserTypeRepository userTypeRepository,
            IMapper mapper,
            ILogger<UserTypeService> logger,
            IValidator<CreateUserTypeDto> createUserTypeDtoValidator,
            IValidator<UpdateUserTypeDto> updateUserTypeDtoValidator,
            IValidator<HardDeleteUserTypeDto> hardDeleteUserTypeDtoValidator,
            IValidator<SoftDeleteUserTypeDto> softDeleteUserTypeDtoValidator,
            IValidator<GetUserTypeDto> getUserTypeDtoValidator)
        {
            // БД contracts
            _userTypeRepository = userTypeRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createUserTypeDtoValidator = createUserTypeDtoValidator;
            _updateUserTypeDtoValidator = updateUserTypeDtoValidator;
            _hardDeleteUserTypeDtoValidator = hardDeleteUserTypeDtoValidator;
            _softDeleteUserTypeDtoValidator = softDeleteUserTypeDtoValidator;
            // R validators
            _getUserTypeDtoValidator = getUserTypeDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateUserTypeDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания UserType.");

            var validation = await _createUserTypeDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var newUserType = new UserType()
            {
                Id = Guid.NewGuid(),
                Type = createDto.Type,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userTypeRepository
                .CreateAsync(newUserType, cancellationToken);

            _logger.LogInformation($"UserType создан с Id: {newUserType.Id}");

            return newUserType.Id;
        }

        public async Task<UserTypeListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения UserTypes, где IsDeleted == false");

            var userTypes = await _userTypeRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("UserTypes, где IsDeleted == false, получены");

            return new UserTypeListVm()
            {
                UserTypeLookups = _mapper.Map<IList<UserTypeLookupDto>>(userTypes)
            };
        }

        public async Task<UserTypeDetailsVm?> GetByIdAsync(
            GetUserTypeDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения UserType по Id: {getDto.Id}");

            var validation = await _getUserTypeDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var userType = await _userTypeRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (userType == null)
            {
                _logger.LogWarning($"UserType не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"UserType найден по Id: {getDto.Id}");

            return _mapper.Map<UserTypeDetailsVm>(userType);
        }

        public async Task HardDeleteAsync(
            HardDeleteUserTypeDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления UserType по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeleteUserTypeDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var userType = await _userTypeRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (userType == null)
            {
                _logger.LogWarning($"UserType не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            // Проверяем, нет ли пользователей с этим типом
            if (userType.Users != null && userType.Users.Any())
            {
                _logger.LogWarning($"Невозможно удалить UserType с Id: {hardDeleteDto.Id}, так как есть связанные пользователи");
                throw new InvalidOperationException("Невозможно удалить тип пользователя, так как есть связанные пользователи");
            }

            await _userTypeRepository
                .HardDeleteAsync(userType, cancellationToken);

            _logger.LogInformation($"UserType полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeleteUserTypeDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления UserType по Id: {softDeleteDto.Id}");

            var validation = await _softDeleteUserTypeDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var userType = await _userTypeRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (userType == null)
            {
                _logger.LogWarning($"UserType не найден по Id: {softDeleteDto.Id}");
                return;
            }

            userType.DeletedAt = DateTime.UtcNow;
            userType.IsDeleted = true;

            await _userTypeRepository
                .SoftDeleteAsync(userType, cancellationToken);

            _logger.LogInformation($"UserType мягко удалён по Id: {softDeleteDto.Id}");
        }

        public async Task UpdateAsync(
            UpdateUserTypeDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало обновления UserType по Id: {updateDto.Id}");

            var validation = await _updateUserTypeDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var userType = await _userTypeRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (userType == null)
            {
                _logger.LogWarning($"UserType не найден по Id: {updateDto.Id}");
                return;
            }

            userType.Type = updateDto.Type;
            userType.UpdatedAt = DateTime.UtcNow;

            await _userTypeRepository
                .UpdateAsync(userType, cancellationToken);

            _logger.LogInformation($"UserType обновлён по Id: {updateDto.Id}");
        }
    }
}
