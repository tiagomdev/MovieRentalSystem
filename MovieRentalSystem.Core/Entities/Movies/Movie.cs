
namespace MovieRentalSystem.Core.Entities.Movies
{
    public class Movie : EntityBase
    {
        public Movie()
        {
        }

        public Movie(string name) : base()
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
