using System;

namespace MovieRentalSystem.Core.Application.DTOs.Movies
{
    public class MovieRentalListDTO
    {
        public Guid Id { get; set; }

        public string MovieName { get; set; }

        public string CustomerName { get; set; }

        public DateTime? ReturnedAt { get; set; }
    }
}
