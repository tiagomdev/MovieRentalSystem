using MovieRentalSystem.Core.Application.DTOs.Movies;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Interfaces.Services.Movies
{
    public interface IMovieService
    {
        Task<MovieDTO> CreateMovieAsync(CreateMovieInputModel inputModel, CancellationToken cancellationToken = default);
    }
}
