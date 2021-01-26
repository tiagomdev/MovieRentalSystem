using MovieRentalSystem.Core.Application.DTOs.Customers;
using MovieRentalSystem.Core.Application.InputModels.Customers;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Interfaces.Services.Customers
{
    public interface ICustomerService
    {
        Task<CustomerDTO> CreateCustomerAsync(CreateCustomerInputModel inputModel, CancellationToken cancellationToken = default);
    }
}
