using System;

namespace MovieRentalSystem.Core.Entities.Movies
{
    public class MovieRental : EntityBase
    {
        public MovieRental()
        {
        }

        public MovieRental(int daysInRental, Guid movieId, Guid customerId)
        {
            DaysInRental = daysInRental;
            MovieId = movieId;
            CustomerId = customerId;
        }

        public int DaysInRental { get; set; }

        public Guid MovieId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime? ReturnedAt { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Customers.Customer Customer { get; set; }

        public void WasReturned()
        {
            ReturnedAt = LastChangedAt = DateTime.UtcNow;
        }
    }
}
