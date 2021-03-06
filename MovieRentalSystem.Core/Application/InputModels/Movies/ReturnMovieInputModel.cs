﻿using MovieRentalSystem.Core.Interfaces.InputModels;
using System;
using System.Collections.Generic;

namespace MovieRentalSystem.Core.Application.InputModels.Movies
{
    public class ReturnMovieInputModel : IInputModel
    {
        public Guid MovieId { get; set; }

        public Guid CustomerId { get; set; }

        public IEnumerable<string> GetErrorMessages()
        {
            if (MovieId == default)
                yield return "MovieId invalid!";
            if (CustomerId == default)
                yield return "CustomerId invalid!";
        }
    }
}
