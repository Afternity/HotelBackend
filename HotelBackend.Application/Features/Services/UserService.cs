using HotelBackend.Application.Common.Contracts.DTOs.UserDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.UserViewModes;
using HotelBackend.Application.Common.Exceptions;
using HotelBackend.Application.Features.InterfacesServices;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AutoMapper;

namespace HotelBackend.Application.Features.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createUserDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateUserDtoValidator;
        private readonly IValidator<FindAndDeleteUserDto> _findAndDeleteUserDtoValidator;

        public UserService(
            IUserRepository userRpository,
            ILogger<UserService> logger,
            IMapper mapper,
            IValidator<CreateUserDto> createUserDtoValidator,
            IValidator<UpdateUserDto> updateUserDtoValidator,
            IValidator<FindAndDeleteUserDto> findAndDeleteUserDtoValidator)
        {
            _userRepository = userRpository;
            _logger = logger;
            _mapper = mapper;
            _createUserDtoValidator = createUserDtoValidator;
            _updateUserDtoValidator = updateUserDtoValidator;
            _findAndDeleteUserDtoValidator = findAndDeleteUserDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateUserDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create user: {nameof(createDto)}");

            var validationResult = await _createUserDtoValidator
                .ValidateAsync(createDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            var createUser = _mapper.Map<User>(createDto);

            _logger.LogInformation($"Validation and Mapping are successful"); 

            var userId = await _userRepository.CreateAsync(createUser, cancellationToken);

            _logger.LogInformation($"User is created, createUser.Id = {userId}");

            return userId;
        }

        public async Task DeleteAsync(
            FindAndDeleteUserDto deleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete user: {nameof(deleteDto)}");

            var validationResult = await _findAndDeleteUserDtoValidator
                .ValidateAsync(deleteDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            var deleteUser = await _userRepository
                .GetByIdAsync(deleteDto.Id, cancellationToken);

            if (deleteUser == null)
                throw new NotFoundException(nameof(deleteUser), deleteDto.Id);

            await _userRepository.DeleteAsync(deleteUser, cancellationToken);

            _logger.LogInformation($"User is deleted, deleteUser.Id = {deleteUser.Id}");
        }

        public async Task<UserListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get All users");

            var users = await _userRepository
                .GetAllAsync(cancellationToken);

            _logger.LogInformation($"Users: {nameof(users)}");

            return new UserListVm()
            {
                Users = _mapper.Map<IList<UserLookupDto>>(users)
            };
        }

        public async Task<UserVm> GetByIdAsync(
            FindAndDeleteUserDto findDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get user: {nameof(findDto)}");

            var validationResult = await _findAndDeleteUserDtoValidator
                .ValidateAsync(findDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            var user = await _userRepository
                .GetByIdAsync(findDto.Id, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), findDto.Id);

            _logger.LogInformation($"User: {user.Id}");

            return _mapper.Map<UserVm>(user);
        }

        public async Task UpdateAsync(
            UpdateUserDto updateDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update user: {nameof(updateDto)}");

            var validationResult = await _updateUserDtoValidator
                .ValidateAsync(updateDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);

            _logger.LogInformation($"Validation is successful");

            var existingUser = await _userRepository
                .GetByIdAsync(updateDto.Id, cancellationToken);

            if (existingUser == null)
                throw new NotFoundException(nameof(existingUser), updateDto.Id);

            var updateUser = _mapper.Map(updateDto, existingUser);

            _logger.LogInformation($"Mapping is successful");

            await _userRepository.UpdateAsync(updateUser, cancellationToken);

            _logger.LogInformation($"User is updated, updateUser.Id = {updateUser.Id}");
        }
    }
}
