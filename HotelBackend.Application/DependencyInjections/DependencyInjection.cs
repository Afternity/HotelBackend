using FluentValidation;
using HotelBackend.Application.Features.Services;
using HotelBackend.Domain.Interfaces.InterfacesServices;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Application.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRoomService, RoomService>();

            return services;
        }
    }
}
