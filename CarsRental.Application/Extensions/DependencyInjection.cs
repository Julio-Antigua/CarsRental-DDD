using CarsRental.Domain.Services.Rentals;
using Microsoft.Extensions.DependencyInjection;

namespace CarsRental.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<PriceService>();

            return services;
        }
    }
}
