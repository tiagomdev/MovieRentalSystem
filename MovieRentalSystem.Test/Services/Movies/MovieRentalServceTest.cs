using Moq;
using MovieRentalSystem.Core.Application.Services.Movies;
using MovieRentalSystem.Core.Entities.Customers;
using MovieRentalSystem.Core.Entities.Movies;
using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Infra.Data.Repositories;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MovieRentalSystem.Test.Services.Movies
{
    public class MovieRentalServceTest : TestBase
    {
        [Test]
        public async Task CreateMovieRentalWithSuccess()
        {
            var movieId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            using (var context = CreateContext())
            {
                await context.AddAsync(new Movie() { Id = movieId });
                await context.AddAsync(new Customer() { Id = customerId });
                await context.SaveChangesAsync();

                var movieRentalService = new MovieRentalService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.CreateMovieRentalInputModel() 
                { 
                    MovieId = movieId,
                    CustomerId = customerId,
                    DaysInRental = 5
                };

                var result = await movieRentalService.CreateMovieRentalAsync(inputModel);

                Assert.AreEqual(inputModel.MovieId, result.MovieId);
                Assert.AreEqual(inputModel.CustomerId, result.CustomerId);
                Assert.AreEqual(inputModel.DaysInRental, result.DaysInRental);
                Assert.IsTrue(result.ReturnedAt == null);
                Assert.IsTrue(result.Id != default);

                ClearContext(context);
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void CatchCreateMovieRentalWithInvalidDaysInRental(int daysInRental)
        {
            var movieRentalService = new MovieRentalService(Mock.Of<IRepository>());

            var inputModel = new Core.Application.InputModels.Movies.CreateMovieRentalInputModel() { DaysInRental = daysInRental };

            Assert.ThrowsAsync<ModelValidationException>(async () => await movieRentalService.CreateMovieRentalAsync(inputModel));
        }

        [Test]
        public async Task CatchCreateMovieRentalWithNonExistentMovie()
        {
            var movieId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            using (var context = CreateContext())
            {
                await context.AddAsync(new Customer() { Id = customerId });
                await context.SaveChangesAsync();

                var movieRentalService = new MovieRentalService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.CreateMovieRentalInputModel()
                {
                    MovieId = movieId,
                    CustomerId = customerId,
                    DaysInRental = 5
                };

                Assert.ThrowsAsync<BusinessException>(async () => await movieRentalService.CreateMovieRentalAsync(inputModel));

                ClearContext(context);
            }
        }

        [Test]
        public async Task CatchCreateMovieRentalWithNonExistentCustomer()
        {
            var movieId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            using (var context = CreateContext())
            {
                await context.AddAsync(new Movie() { Id = movieId });
                await context.SaveChangesAsync();

                var movieRentalService = new MovieRentalService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.CreateMovieRentalInputModel()
                {
                    MovieId = movieId,
                    CustomerId = customerId,
                    DaysInRental = 5
                };

                Assert.ThrowsAsync<BusinessException>(async () => await movieRentalService.CreateMovieRentalAsync(inputModel));

                ClearContext(context);
            }
        }

        [Test]
        public async Task CatchCreateMovieRentalWithNotAvailableMovie()
        {
            var movieId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            using (var context = CreateContext())
            {
                await context.AddAsync(new Customer() { Id = customerId });
                await context.AddAsync(new Movie() { Id = movieId });
                await context.AddAsync(new MovieRental() { MovieId = movieId });
                await context.SaveChangesAsync();

                var movieRentalService = new MovieRentalService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.CreateMovieRentalInputModel()
                {
                    MovieId = movieId,
                    CustomerId = customerId,
                    DaysInRental = 5
                };

                Assert.ThrowsAsync<BusinessException>(async () => await movieRentalService.CreateMovieRentalAsync(inputModel));

                ClearContext(context);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ReturnMovieWithSuccess(bool passedDeliveryDay)
        {
            var movieId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var daysInRental = 5;
            DateTime createdAt = DateTime.UtcNow.AddDays(-1);
            if (passedDeliveryDay)
                createdAt = createdAt.AddDays(-daysInRental);

            using (var context = CreateContext())
            {
                await context.AddAsync(new Movie() { Id = movieId });
                await context.AddAsync(new Customer() { Id = customerId });
                await context.AddAsync(new MovieRental() { MovieId = movieId, CustomerId = customerId, DaysInRental = daysInRental, CreatedAt = createdAt });
                await context.SaveChangesAsync();

                var movieRentalService = new MovieRentalService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.ReturnMovieInputModel()
                {
                    MovieId = movieId,
                    CustomerId = customerId,
                };

                var result = await movieRentalService.ReturnMovieAsync(inputModel);

                var expectedStatus = passedDeliveryDay ? "Alert" : "Success";

                Assert.IsTrue(result.Status.Equals(expectedStatus));

                ClearContext(context);
            }
        }
    }
}
