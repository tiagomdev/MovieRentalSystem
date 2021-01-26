using MovieRentalSystem.Core.Application.DTOs.Movies;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Interfaces.Services.Movies
{
    public interface IMovieRentalService
    {
        Task<MovieRentalDTO> CreateMovieRentalAsync(CreateMovieRentalInputModel inputModel, CancellationToken cancellationToken = default);

        Task<MovieRentalReturnedReponsesDTO> ReturnMovieAsync(ReturnMovieInputModel inputModel, CancellationToken cancellationToken = default);

        Task<IList<MovieRentalListDTO>> FindAll(CancellationToken cancellationToken = default);
    }
}
