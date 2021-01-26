using MovieRentalSystem.Core.Application.DTOs.Movies;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using MovieRentalSystem.Core.Entities.Movies;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Core.Interfaces.Services.Movies;
using MovieRentalSystem.Core.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Application.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IRepository _repository;
        public MovieService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieDTO> CreateMovieAsync(CreateMovieInputModel inputModel, CancellationToken cancellationToken = default)
        {
            inputModel.EnsureIsValid();

            var movie = new Movie(inputModel.Name);

            await _repository.AddAsync(movie, cancellationToken);

            return new MovieDTO()
            {
                Id = movie.Id,
                Name = movie.Name
            };
        }
    }
}
