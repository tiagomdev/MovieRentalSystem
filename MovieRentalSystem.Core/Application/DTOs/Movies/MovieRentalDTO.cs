using System;

namespace MovieRentalSystem.Core.Application.DTOs.Movies
{
    public class MovieRentalDTO
    {
        public Guid Id { get; set; }

        public int DaysInRental { get; set; }

        public Guid MovieId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime? ReturnedAt { get; set; }
    }
}
