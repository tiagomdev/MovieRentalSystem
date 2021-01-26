using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Core.Entities.Customers;
using MovieRentalSystem.Core.Entities.Movies;
using MovieRentalSystem.Infra.Data.Mappings;

namespace MovieRentalSystem.Infra.Data.Context
{
    public class MovieRentalSystemContext : DbContext
    {
        public MovieRentalSystemContext(DbContextOptions<MovieRentalSystemContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieRental> MovieRentals { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieRentalMapping());
        }
    }
}
