using FluentValidation;
using HotelBackend.Identity.Common.Extentions;
using HotelBackend.Identity.Common.Interfaces;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Identity.Common.Settings;
using HotelBackend.Identity.Data.DbContexts;
using HotelBackend.Shared.Contracts.Containers.IdentityContainers;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.AuthorizationDTOs;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.RegistrationDTOs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.AuthorizationVMs;
using HotelBackend.Shared.Contracts.VMs.IdentityVMs.RegistrationVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Web;

namespace HotelBackend.Identity.Services
{
    /// <summary>
    /// На будующее:
    ///     1. Разобраться с JwtTokensContainer, сделать его частью всех Vm как valie object
    ///     
    /// </summary>
    public class IdentityService
        : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HotelBackendIdentityDbContext _context;

        private readonly IValidator<RegistrationDto> _registrationDtoValidator;
        private readonly IValidator<JwtTokensContainer> _tokenModelValidator;
        private readonly IValidator<AuthorizationDto> _authorizationDtoValidator;

        private readonly IJwtTokenService _jwtTokenService;
        private readonly IEmailSender _emailSender;

        private readonly JwtSettings _jwtSettings;

        private readonly ILogger<IdentityService> _logger;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            HotelBackendIdentityDbContext context,
            IValidator<RegistrationDto> registrationDtoValidator,
            IValidator<JwtTokensContainer> tokenModelValidator,
            IValidator<AuthorizationDto> authorizationDtoValidator,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration,
            IOptionsMonitor<JwtSettings> jwtSettings,
            IEmailSender emailSender,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _context = context;

            _registrationDtoValidator = registrationDtoValidator;
            _tokenModelValidator = tokenModelValidator;
            _authorizationDtoValidator = authorizationDtoValidator;

            _jwtTokenService = jwtTokenService;
            _emailSender = emailSender;

            _jwtSettings = jwtSettings.CurrentValue
                ?? throw new ArgumentNullException(nameof(jwtSettings));

            _logger = logger;
        }

        public async Task<AuthorizationVm> AuthorizationAsync(
            AuthorizationDto authorizationDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало авторизации: {authorizationDto.Email}");

            var validation = await _authorizationDtoValidator
                .ValidateAsync(authorizationDto, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"Валидация не пройдена: {string.Join(", ", validation.Errors)}");
                throw new ValidationException(validation.Errors);
            }

            var user = await _userManager.FindByEmailAsync(authorizationDto.Email);

            if (user == null)
            {
                _logger.LogWarning($"Пользователь не найден: {authorizationDto.Email}");
                throw new AuthenticationException($"Не верный email");
            }

            var isLockedOut = await _userManager.IsLockedOutAsync(user);

            if (isLockedOut)
            {
                _logger.LogWarning($"Заблокированный пользователь пытается войти: {authorizationDto.Email}");
                throw new AuthenticationException("Аккаунт заблокирован");
            }

            if (!user.EmailConfirmed)
            {
                _logger.LogWarning($"Попытка входа без подтвержденного email: {authorizationDto.Email}");
                throw new AuthenticationException("Подтвердите email для входа");
            }

            var isPasswordValid = await _userManager
                .CheckPasswordAsync(user, authorizationDto.Password);

            if (!isPasswordValid)
            {
                _logger.LogWarning($"Не верный пароль для пользователя: {authorizationDto.Email}");

                await _userManager.AccessFailedAsync(user);

                var failedCount = await _userManager.GetAccessFailedCountAsync(user);

                if (failedCount >= 5)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddMinutes(15));
                    _logger.LogWarning($"Пользователь заблокирован после 5 неудачных попыток: {authorizationDto.Email}");
                    throw new AuthenticationException("Аккаунт временно заблокирован");
                }

                throw new AuthenticationException("Не верный пароль");
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user, roles);
            var refreshToken = _jwtTokenService.CreateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = _jwtTokenService.RefreshTokenValidityInDays;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Успешная авторизация: {authorizationDto.Email}");

            return new AuthorizationVm
            {
                Tokens = new JwtTokensContainer(accessToken, refreshToken)
            };
        }

        public async Task<RegistrationVm> RegistrationAsync(
            RegistrationDto registrationDto,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало регистрации: {registrationDto.Email}");

            var validation = await _registrationDtoValidator
                .ValidateAsync(registrationDto, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"Валидация не пройдена {string.Join(", ", validation.Errors)}");
                throw new ValidationException(validation.Errors);
            }

            var existingUser = await _userManager.FindByEmailAsync(registrationDto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning($"Пользователь уже существует: {registrationDto.Email}");
                throw new ValidationException("Пользователь с таким email уже существует");
            }

            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userInTransaction = await _context.Users
                        .FirstOrDefaultAsync(u => u.Email == registrationDto.Email, cancellationToken);

                    if (userInTransaction != null)
                    {
                        throw new ValidationException("Пользователь с таким email уже существует");
                    }

                    var newUser = new ApplicationUser
                    {
                        FirstName = registrationDto.FirstName,
                        LastName = registrationDto.LastName,
                        MiddleName = registrationDto.MiddleName,
                        Email = registrationDto.Email,
                        UserName = registrationDto.Email,
                        EmailConfirmed = false,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var createResult = await _userManager.CreateAsync(newUser, registrationDto.Password);
                    if (!createResult.Succeeded)
                    {
                        _logger.LogError($"Ошибка создания пользователя: {string.Join(", ", createResult.Errors)}");
                        throw new ValidationException($"Ошибка создания пользователя: {string.Join(", ", createResult.Errors)}");
                    }

                    await _userManager.AddToRoleAsync(newUser, RoleConsts.Member);

                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var encodedToken = HttpUtility.UrlEncode(confirmationToken);

                    var confirmationLink = $"http://localhost:5078/api/Identity/confirm-email?userId={newUser.Id}&token={encodedToken}";

                    await _emailSender.SendAsync(
                        to: registrationDto.Email,
                        subject: "Подтвердите ваш email",
                        body: $"<p>Пожалуйста, подтвердите ваш email:</p>" +
                              $"<a href='{confirmationLink}'>Подтвердить email</a>" +
                              $"<p>Ссылка действительна 24 часа.</p>",
                        cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    _logger.LogInformation($"Регистрация завершена: {registrationDto.Email}");

                    return new RegistrationVm
                    {
                        UserName = newUser.UserName!,
                        Email = newUser.Email!,
                        EmailConfirmationRequired = true,
                        Message = "Регистрация успешна. Проверьте email для подтверждения."
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

        public async Task<bool> ConfirmEmailAsync(
            string userId,
            string token,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"Пользователь не найден для подтверждения: {userId}");
                return false;
            }

            if (user.EmailConfirmed)
            {
                _logger.LogInformation($"Email уже подтвержден: {user.Email}");
                return true;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                _logger.LogWarning($"Ошибка подтверждения email: {user.Email}");
                return false;
            }

            _logger.LogInformation($"Email подтвержден: {user.Email}");
            return true;
        }

        public async Task<bool> ResendConfirmationEmailAsync(
            string email,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null ||
                user.EmailConfirmed)
            {
                _logger.LogWarning($"Email подтверждения не требуется для {email}");
                return true;
            }

            var lastSent = await _context.UserClaims
                .Where(c => c.UserId == user.Id && c.ClaimType == "ConfirmationEmailSent")
                .OrderByDescending(c => c.ClaimValue)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastSent != null &&
                DateTime.TryParse(lastSent.ClaimValue, out var sentTime) &&
                sentTime > DateTime.UtcNow.AddMinutes(-5))
            {
                throw new InvalidOperationException("Письмо уже отправлено. Подождите 5 минут.");
            }

            var token = await _userManager
                .GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            var confirmationLink = $"http://localhost:5078/api/Identity/confirm-email?userId={user.Id}&token={encodedToken}";

            await _emailSender.SendAsync(
                to: user.Email ?? throw new ArgumentException("У пользовалетеля отсутствует email"),
                subject: "Подтвердите ваш email",
                body: $"<p>Пожалуйста, подтвердите ваш email:</p>" +
                          $"<a href='{confirmationLink}'>Подтвердить email</a>" +
                          $"<p>Ссылка действительна 24 часа.</p>",
                cancellationToken);

            await _userManager.AddClaimAsync(user,
                new Claim("ConfirmationEmailSent", DateTime.UtcNow.ToString("O")));

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<JwtTokensContainer> RefreshTokenAsync(
           JwtTokensContainer jwtTokenContainer,
           CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало пересоздания токена");

            var validation = await _tokenModelValidator
                .ValidateAsync(jwtTokenContainer, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"Валидания не пройдена: {string.Join(", ", validation.Errors)}");
                throw new ValidationException(validation.Errors);
            }

            var principal = _jwtTokenService.ValidateExpiredAccessToken(jwtTokenContainer.AccessToken);

            if (principal == null)
            {
                _logger.LogWarning("Недействительный основной токен для обновления");
                throw new AuthenticationException("Недействительный основной токен для обновления");
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                _logger.LogWarning("Id пользователя не найдено");
                throw new AuthenticationException("Id пользователя не найдено");
            }

            _logger.LogInformation($"Refresh token для пользователя: {userId}");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("По сновному токену пользователь не найден");
                throw new AuthenticationException("По сновному токену пользователь не найден");
            }

            var isValidRefreshToken = IJwtTokenService
               .CheckValidRefreshToken(
                    user.RefreshToken!,
                    user.RefreshTokenExpiryTime,
                    jwtTokenContainer.RefreshToken);

            if (!isValidRefreshToken)
            {
                _logger.LogWarning($"Недействительный основной токен для пользователя: {principal.Identity!.Name}");
                throw new AuthenticationException($"Недействительный основной токен для пользователя: {principal.Identity!.Name}");
            }

            var newAccessToken = _jwtTokenService.CreateAccessToken(principal);
            var newRefreshToken = _jwtTokenService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            // а почему сдесь нет SaveChangesAsync как в методе AuthorizationAsync

            _logger.LogInformation($"Конец пересоздания токена,");

            return new JwtTokensContainer(newAccessToken, newRefreshToken);
        }

        public async Task RevokeAsync(
            string userEmail,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало пересоздания токена, email: {userEmail}");

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                _logger.LogWarning($"Пользователь с email {userEmail} не найден");
                throw new ArgumentException($"Пользователь с email {userEmail} не найден");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            _logger.LogInformation($"Конец пересоздания токена, email: {userEmail}");
        }

        public async Task RevokeAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Начало пересоздания всех токенов");

            var users = await _userManager.Users.ToListAsync(cancellationToken);

            if (users.Count == 0)
            {
                _logger.LogWarning($"Нет пользователей, проверьте БД");
                return;
            }

            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            _logger.LogInformation("Все токены пересозданы");
        }
    }
}
