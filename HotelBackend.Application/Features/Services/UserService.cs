using AutoMapper;
using FluentValidation;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.CreateUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.GetUserDTOs;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;
using HotelBackend.Shared.Contracts.VMs.UserViewModes.UserListVMs;
using HotelBackend.Shared.Contracts.VMs.UserVMs.UserDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Services
{
    public class UserService
        : IUserService
    {
        // БД contracts
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        // infrastructure
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        // CUD validators
        private readonly IValidator<CreateUserDto> _createUserDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateUserDtoValidator;
        private readonly IValidator<HardDeleteUserDto> _hardDeleteUserDtoValidator;
        private readonly IValidator<SoftDeleteUserDto> _softDeleteUserDtoValidator;
        // R validators
        private readonly IValidator<GetUserDto> _getUserDtoValidator;

        public UserService(
            IUserRepository userRepository,
            IUserTypeRepository userTypeRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            IValidator<CreateUserDto> createUserDtoValidator,
            IValidator<UpdateUserDto> updateUserDtoValidator,
            IValidator<HardDeleteUserDto> hardDeleteUserDtoValidator,
            IValidator<SoftDeleteUserDto> softDeleteUserDtoValidator,
            IValidator<GetUserDto> getUserDtoValidator)
        {
            // БД contracts
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            // infrastructure
            _mapper = mapper;
            _logger = logger;
            // CUD validators
            _createUserDtoValidator = createUserDtoValidator;
            _updateUserDtoValidator = updateUserDtoValidator;
            _hardDeleteUserDtoValidator = hardDeleteUserDtoValidator;
            _softDeleteUserDtoValidator = softDeleteUserDtoValidator;
            // R validators
            _getUserDtoValidator = getUserDtoValidator;
        }

        /// <summary>
        /// Авторизация. Надо поменять реализацию default UserType.
        /// </summary>
        /// <param name="createDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task<Guid> CreateAsync(
            CreateUserDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало создания User.");

            var validation = await _createUserDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            // По умолчанию устанавливаем обычного пользователя (предположим, что Id = 1)
            var defaultUserType = await _userTypeRepository
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000001"), cancellationToken);

            if (defaultUserType == null)
            {
                _logger.LogWarning($"UserType по умолчанию не найден");
                return Guid.Empty;
            }

            var newUser = new User()
            {
                Id = Guid.NewGuid(),

                Name = createDto.Name,
                Email = createDto.Email,
                Password = createDto.Password, 

                UserTypeId = defaultUserType.Id,

                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userRepository
                .CreateAsync(newUser, cancellationToken);

            _logger.LogInformation($"User создан с Id: {newUser.Id}");

            return newUser.Id;
        }

        public async Task<UserListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения Users, где IsDeleted == false");

            var users = await _userRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation("Users, где IsDeleted == false, получены");

            return new UserListVm()
            {
                UserLookups = _mapper.Map<IList<UserLookupDto>>(users)
            };
        }

        public async Task<UserListVm> GetAllByBookingAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начало получения Users с бронированиями, где IsDeleted == false");

            var users = await _userRepository
                .GetAllByBookingAsync(cancellationToken);

            _logger.LogInformation("Users с бронированиями, где IsDeleted == false, получены");

            return new UserListVm()
            {
                UserLookups = _mapper.Map<IList<UserLookupDto>>(users)
            };
        }

        public async Task<UserDetailsVm?> GetByIdAsync(
            GetUserDto getDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало получения User по Id: {getDto.Id}");

            var validation = await _getUserDtoValidator
                .ValidateAsync(getDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var user = await _userRepository
                .GetByIdAsync(getDto.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning($"User не найден по Id: {getDto.Id}");
                return null;
            }

            _logger.LogInformation($"User найден по Id: {getDto.Id}");

            return _mapper.Map<UserDetailsVm>(user);
        }

        public async Task HardDeleteAsync(
            HardDeleteUserDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало полного удаления User по Id: {hardDeleteDto.Id}");

            var validation = await _hardDeleteUserDtoValidator
                .ValidateAsync(hardDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var user = await _userRepository
                .GetByIdAsync(hardDeleteDto.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning($"User не найден по Id: {hardDeleteDto.Id}");
                return;
            }

            await _userRepository
                .HardDeleteAsync(user, cancellationToken);

            _logger.LogInformation($"User полностью удален по Id: {hardDeleteDto.Id}");
        }

        public async Task SoftDeleteAsync(
            SoftDeleteUserDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало мягкого удаления User по Id: {softDeleteDto.Id}");

            var validation = await _softDeleteUserDtoValidator
                .ValidateAsync(softDeleteDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var user = await _userRepository
                .GetByIdAsync(softDeleteDto.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning($"User не найден по Id: {softDeleteDto.Id}");
                return;
            }

            user.DeletedAt = DateTime.UtcNow;
            user.IsDeleted = true;

            await _userRepository
                .SoftDeleteAsync(user, cancellationToken);

            _logger.LogInformation($"User мягко удалён по Id: {softDeleteDto.Id}");
        }

        /// <summary>
        /// Обновление профиля.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task UpdateAsync(
            UpdateUserDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало обновления User по Id: {updateDto.Id}");

            var validation = await _updateUserDtoValidator
                .ValidateAsync(updateDto, cancellationToken);

            if (validation.IsValid == false)
                throw new ValidationException(validation.Errors);

            var user = await _userRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning($"User не найден по Id: {updateDto.Id}");
                return;
            }

            user.Name = updateDto.Name;
            user.Email = updateDto.Email;
            user.Password = updateDto.Password; 

            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository
                .UpdateAsync(user, cancellationToken);

            _logger.LogInformation($"User обновлён по Id: {updateDto.Id}");
        }
    }
}
