using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.UserTypeViewModels;
using HotelBackend.Application.Common.Exceptions;
using HotelBackend.Application.Features.InterfacesServices;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AutoMapper;


namespace HotelBackend.Application.Features.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly ILogger<UserTypeService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserTypeDto> _createUserTypeDtoValidator;
        private readonly IValidator<UpdateUserTypeDto> _updateUserTypeDtoValidator;
        private readonly IValidator<FindAndDeleteUserTypeDto> _findAndDeleteUserTypeDtoValidator;

        public UserTypeService(
            IUserTypeRepository userTypeRepository,
            ILogger<UserTypeService> logger,
            IMapper mapper,
            IValidator<CreateUserTypeDto> createUserTypeDtoValidator,
            IValidator<UpdateUserTypeDto> updateUserTypeDtoValidator,
            IValidator<FindAndDeleteUserTypeDto> findAndDeleteUserTypeDtoValidator)
        {
            _userTypeRepository = userTypeRepository;
            _logger = logger;
            _mapper = mapper;
            _createUserTypeDtoValidator = createUserTypeDtoValidator;
            _updateUserTypeDtoValidator = updateUserTypeDtoValidator;
            _findAndDeleteUserTypeDtoValidator = findAndDeleteUserTypeDtoValidator;
        }

        public async Task<Guid> CreateAsync(
            CreateUserTypeDto createDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Create UserType: {nameof(createDto)}");

            var validationResult = await _createUserTypeDtoValidator.ValidateAsync(createDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var userType = _mapper.Map<UserType>(createDto);
            var userTypeId = await _userTypeRepository.CreateAsync(userType, cancellationToken);

            _logger.LogInformation($"UserType created, ID: {userTypeId}");
            return userTypeId;
        }

        public async Task DeleteAsync(
            FindAndDeleteUserTypeDto deleteDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete UserType: {nameof(deleteDto)}");

            var validationResult = await _findAndDeleteUserTypeDtoValidator.ValidateAsync(deleteDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var userType = await _userTypeRepository.GetByIdAsync(deleteDto.Id, cancellationToken);
            if (userType == null)
                throw new NotFoundException(nameof(UserType), deleteDto.Id);

            await _userTypeRepository.DeleteAsync(userType, cancellationToken);
            _logger.LogInformation($"UserType deleted, ID: {deleteDto.Id}");
        }

        public async Task<UserTypeListVm> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all UserTypes");

            var userTypes = await _userTypeRepository.GetAllAsync(cancellationToken);
            var result = new UserTypeListVm
            {
                UserTypes = _mapper.Map<IList<UserTypeLookupDto>>(userTypes)
            };

            _logger.LogInformation($"Retrieved {userTypes.Count} UserTypes");
            return result;
        }

        public async Task<UserTypeVm> GetByIdAsync(
            FindAndDeleteUserTypeDto findDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get UserType: {nameof(findDto)}");

            var validationResult = await _findAndDeleteUserTypeDtoValidator.ValidateAsync(findDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var userType = await _userTypeRepository.GetByIdAsync(findDto.Id, cancellationToken);
            if (userType == null)
                throw new NotFoundException(nameof(UserType), findDto.Id);

            _logger.LogInformation($"UserType found, ID: {findDto.Id}");
            return _mapper.Map<UserTypeVm>(userType);
        }

        public async Task UpdateAsync(
            UpdateUserTypeDto updateDto, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update UserType: {nameof(updateDto)}");

            var validationResult = await _updateUserTypeDtoValidator.ValidateAsync(updateDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUserType = await _userTypeRepository.GetByIdAsync(updateDto.Id, cancellationToken);
            if (existingUserType == null)
                throw new NotFoundException(nameof(UserType), updateDto.Id);

            var updatedUserType = _mapper.Map(updateDto, existingUserType);
            await _userTypeRepository.UpdateAsync(updatedUserType, cancellationToken);

            _logger.LogInformation($"UserType updated, ID: {updateDto.Id}");
        }
    }
}
