using Moq;
using MovieRentalSystem.Core.Application.Services.Movies;
using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Infra.Data.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieRentalSystem.Test.Services.Movies
{
    public class MovieServceTest : TestBase
    {
        [Test]
        public async Task CreateMovieWithSuccess()
        {
            using(var context = CreateContext())
            {
                var movieService = new MovieService(new RepositoryBase(context));

                var inputModel = new Core.Application.InputModels.Movies.CreateMovieInputModel() { Name = "Test" };

                var result = await movieService.CreateMovieAsync(inputModel);

                Assert.AreEqual(inputModel.Name, result.Name);
                Assert.IsTrue(result.Id != default);

                ClearContext(context);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CatchCreateMovieWithInvalidName(string name)
        {
            var movieService = new MovieService(Mock.Of<IRepository>());

            var inputModel = new Core.Application.InputModels.Movies.CreateMovieInputModel() { Name = name };

            Assert.ThrowsAsync<ModelValidationException>(async () => await movieService.CreateMovieAsync(inputModel));
        }
    }
}
