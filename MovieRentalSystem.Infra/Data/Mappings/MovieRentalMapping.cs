using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRentalSystem.Core.Entities.Movies;

namespace MovieRentalSystem.Infra.Data.Mappings
{
    public class MovieRentalMapping : IEntityTypeConfiguration<MovieRental>
    {
        public void Configure(EntityTypeBuilder<MovieRental> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DaysInRental).IsRequired();
            builder.Property(p => p.MovieId).IsRequired();
            builder.Property(p => p.CustomerId).IsRequired();

            builder.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId);
            builder.HasOne(m => m.Movie).WithMany().HasForeignKey(m => m.MovieId);
        }
    }
}
