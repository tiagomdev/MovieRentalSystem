using System.Collections.Generic;

namespace MovieRentalSystem.Core.Interfaces.InputModels
{
    public interface IInputModel
    {
        IEnumerable<string> GetErrorMessages();
    }
}
