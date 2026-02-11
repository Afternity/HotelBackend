using FluentValidation;
using HotelBackend.Identity.Common.Interfaces;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Identity.Common.Settings;
using HotelBackend.Identity.Data.DbContexts;
using HotelBackend.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelBackend.Identity.DependecyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration config)
        {
            AddDbContext(services, config);
            AddIdentityCore(services);
            AddAuthetication(services, config);
            AddAuthorization(services);
            AddReadyMadeServices(services);
            AddCustomServices(services);
            AddConfiguration(services, config);

            return services;
        }

        private static void AddConfiguration(
            IServiceCollection services,
            IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("Jwt"));
            services.Configure<EmailSenderSettings>(
                config.GetSection("EmailSenderSettings:HotelBackendCompany"));
        }

        private static void AddIdentityCore(
            IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<HotelBackendIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>();
        }

        private static void AddAuthorization(
            IServiceCollection services)
        {
            services.AddAuthorization(options => options.DefaultPolicy =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                        .Build());
        }

        private static void AddDbContext(
           IServiceCollection services,
           IConfiguration config)
        {
            var connectionString = config.GetConnectionString("IdentityConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'IdentityConnection' not found");

            services.AddDbContext<HotelBackendIdentityDbContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("HotelBackend.Identity");

                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);

                    npgsqlOptions.CommandTimeout(30);
                });

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });
        }

        private static void AddReadyMadeServices(
            IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        }

        private static void AddCustomServices(
            IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IIdentityService, IdentityService>();
        }

        private static void AddAuthetication(
            IServiceCollection services,
            IConfiguration config)
        {
            var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>();

            if (jwtSettings == null)
                throw new ArgumentNullException(nameof(jwtSettings));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer 
                        ?? throw new ArgumentNullException(jwtSettings.Issuer),
                    ValidAudience = jwtSettings.Audience 
                        ?? throw new ArgumentNullException(jwtSettings.Audience),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret 
                            ?? throw new ArgumentNullException(jwtSettings.Secret))),
                };
            });
        }
    }
}
