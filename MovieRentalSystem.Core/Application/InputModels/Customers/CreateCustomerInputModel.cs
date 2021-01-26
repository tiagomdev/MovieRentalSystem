using MovieRentalSystem.Core.Interfaces.InputModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.Core.Application.InputModels.Customers
{
    public class CreateCustomerInputModel : IInputModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> GetErrorMessages()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return "Name invalid!";
            if(string.IsNullOrWhiteSpace(Email) || new EmailAddressAttribute().IsValid(Email) is false)
                yield return "Email invalid!";
        }
    }
}
