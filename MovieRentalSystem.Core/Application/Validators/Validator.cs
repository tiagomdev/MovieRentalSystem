using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.InputModels;
using System.Linq;

namespace MovieRentalSystem.Core.Validators
{
    public static class Validator
    {
        public static void EnsureIsValid(this IInputModel command)
        {
            var errorMesages = command.GetErrorMessages();

            if (errorMesages != null && errorMesages.Count() > 0)
            {
                string messageToException = "";

                foreach (var item in errorMesages)
                {
                    messageToException += item + "\n";
                }

                throw new ModelValidationException(messageToException);
            }
        }
    }
}
