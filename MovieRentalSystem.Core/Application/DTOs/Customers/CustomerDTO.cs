using System;

namespace MovieRentalSystem.Core.Application.DTOs.Customers
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
