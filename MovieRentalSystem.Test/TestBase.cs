using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieRentalSystem.Infra.Data.Context;
using System;
using System.Runtime.CompilerServices;

namespace MovieRentalSystem.Test
{
    public class TestBase
    {
        private static DbContextOptions<MovieRentalSystemContext> GetDefaultContextOption(string databaseName)
        {
            return new DbContextOptionsBuilder<MovieRentalSystemContext>()
               .UseInMemoryDatabase(databaseName)
               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;
        }

        protected MovieRentalSystemContext CreateContext([CallerMemberName] string databaseName = null) => new MovieRentalSystemContext(GetDefaultContextOption(databaseName ?? throw new ArgumentNullException(nameof(databaseName))));

        protected async void ClearContext(MovieRentalSystemContext context) => await context.Database.EnsureDeletedAsync();
    }
}
