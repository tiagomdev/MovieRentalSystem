using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using MovieRentalSystem.Core.Interfaces.Services.Movies;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieRentalController : ControllerBase
    {
        private readonly IMovieRentalService _movieRentalService;

        public MovieRentalController(IMovieRentalService movieRentalService)
        {
            _movieRentalService = movieRentalService;
        }

        [HttpPost]
        [Route("/rent")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRentalInputModel inputModel, 
            CancellationToken cancellationToken = default)
        {
            var movieRentalCreated = await _movieRentalService.CreateMovieRentalAsync(inputModel, cancellationToken);

            return Created($"api/movies/rent/{movieRentalCreated.Id}", movieRentalCreated);
        }

        [HttpPut]
        [Route("/return")]
        public async Task<IActionResult> ReturnMovieAsync([FromBody] ReturnMovieInputModel inputModel,
            CancellationToken cancellationToken = default)
        {
            var result = await _movieRentalService.ReturnMovieAsync(inputModel, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("/rented")]
        public async Task<IActionResult> FindAll(CancellationToken cancellationToken = default)
        {
            var result = await _movieRentalService.FindAll(cancellationToken);

            return Ok(result);
        }
    }
}
