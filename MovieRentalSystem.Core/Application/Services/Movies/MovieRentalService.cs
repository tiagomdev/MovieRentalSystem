using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Core.Application.DTOs.Movies;
using MovieRentalSystem.Core.Application.InputModels.Movies;
using MovieRentalSystem.Core.Entities.Customers;
using MovieRentalSystem.Core.Entities.Movies;
using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Core.Interfaces.Services.Movies;
using MovieRentalSystem.Core.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Application.Services.Movies
{
    public class MovieRentalService : IMovieRentalService
    {
        private readonly IRepository _repository;
        public MovieRentalService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieRentalDTO> CreateMovieRentalAsync(CreateMovieRentalInputModel inputModel, CancellationToken cancellationToken = default)
        {
            inputModel.EnsureIsValid();

            var movie = await _repository.Query<Movie>().FirstOrDefaultAsync(c => c.Id == inputModel.MovieId, cancellationToken);

            if (movie is null)
                throw new BusinessException("Movie sented not exist");

            var customer = await _repository.Query<Customer>().FirstOrDefaultAsync(c => c.Id == inputModel.CustomerId, cancellationToken);

            if (customer is null)
                throw new BusinessException("Customer sented not exist");

            var movieRental = await _repository.Query<MovieRental>().FirstOrDefaultAsync(c => c.MovieId == inputModel.MovieId && c.ReturnedAt == null, cancellationToken);

            if(movieRental != null)
                throw new BusinessException("Movie not available to rent.");

            movieRental = new MovieRental(inputModel.DaysInRental, inputModel.MovieId, inputModel.CustomerId) { DaysInRental = inputModel.DaysInRental };

            await _repository.AddAsync(movieRental, cancellationToken);

            return new MovieRentalDTO()
            {
                Id = movieRental.Id,
                MovieId = movieRental.MovieId,
                CustomerId = movieRental.CustomerId,
                ReturnedAt = movieRental.ReturnedAt,
                DaysInRental = movieRental.DaysInRental
            };
        }

        public async Task<MovieRentalReturnedReponsesDTO> ReturnMovieAsync(ReturnMovieInputModel inputModel, CancellationToken cancellationToken = default)
        {
            inputModel.EnsureIsValid();

            var movieRental = await _repository.Query<MovieRental>().FirstOrDefaultAsync(c => c.MovieId == inputModel.MovieId 
            && c.CustomerId == inputModel.CustomerId  && c.ReturnedAt == null, cancellationToken);

            if (movieRental == null)
                return null;

            movieRental.WasReturned();

            await _repository.UpdateAsync(movieRental, cancellationToken);

            var passedDeliveryDay = movieRental.ReturnedAt > movieRental.CreatedAt.AddDays(movieRental.DaysInRental);

            return new MovieRentalReturnedReponsesDTO(passedDeliveryDay);
        }

        //criei essa função para poder conferir se estavam salvando todas as locações corretamente in memory
        public async Task<IList<MovieRentalListDTO>> FindAll(CancellationToken cancellationToken = default)
        {
            var result = await _repository.Query<MovieRental>().Include(m => m.Movie).Include(m => m.Customer)
                .Select(m => new MovieRentalListDTO()
            {
                    Id = m.Id,
                    MovieName = m.Movie.Name,
                    CustomerName = m.Customer.Name,
                    ReturnedAt = m.ReturnedAt
            }).ToListAsync(cancellationToken);

            return result;
        }
    }
}
