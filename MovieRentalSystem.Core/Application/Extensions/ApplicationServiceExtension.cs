using Microsoft.Extensions.DependencyInjection;
using MovieRentalSystem.Core.Application.Services.Customers;
using MovieRentalSystem.Core.Application.Services.Movies;
using MovieRentalSystem.Core.Interfaces.Services.Customers;
using MovieRentalSystem.Core.Interfaces.Services.Movies;

namespace MovieRentalSystem.Core.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMovieRentalService, MovieRentalService>();

            return services;
        }
    }
}
