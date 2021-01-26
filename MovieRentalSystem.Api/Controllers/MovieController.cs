using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using MovieRentalSystem.Core.Interfaces.Services.Movies;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieInputModel inputModel, 
            CancellationToken cancellationToken = default)
        {
            var movieCreated = await _movieService.CreateMovieAsync(inputModel, cancellationToken);

            return Created($"api/movies/{movieCreated.Id}", movieCreated);
        }
    }
}
