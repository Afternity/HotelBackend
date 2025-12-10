using FluentValidation;
using HotelBackend.Application.Features.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Application.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            //services.AddScoped<IRoomService, RoomService>();
            //services.AddScoped<IReservationService, ReservationService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserTypeService, UserTypeService>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
