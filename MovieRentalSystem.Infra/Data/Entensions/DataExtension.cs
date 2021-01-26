using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Infra.Data.Context;
using MovieRentalSystem.Infra.Data.Repositories;

namespace MovieRentalSystem.Infra.Data.Entensions
{
    public static class DataExtension
    {
        public static void AddContext(this IServiceCollection services)
        {
            var options = new DbContextOptionsBuilder<MovieRentalSystemContext>()
               .UseInMemoryDatabase("MovieRentalSystem")
               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;

            var context = new MovieRentalSystemContext(options);

            //coloquei Singleton nesse caso pelo fato de está trabalhando com os dados in memory.
            services.AddSingleton(context);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, RepositoryBase>();

            return services;
        }
    }
}
