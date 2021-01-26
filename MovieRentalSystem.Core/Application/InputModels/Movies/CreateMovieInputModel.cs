using MovieRentalSystem.Core.Interfaces.InputModels;
using System.Collections.Generic;

namespace MovieRentalSystem.Core.Application.InputModels.Movies
{
    public class CreateMovieInputModel : IInputModel
    {
        public string Name { get; set; }

        public IEnumerable<string> GetErrorMessages()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return "Name invalid!";
        }
    }
}
