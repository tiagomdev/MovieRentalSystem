using System;

namespace MovieRentalSystem.Core.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string message) : base(message)
        {
        }
    }
}
