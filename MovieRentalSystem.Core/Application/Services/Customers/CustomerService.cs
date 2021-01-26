using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Core.Application.DTOs.Customers;
using MovieRentalSystem.Core.Application.InputModels.Customers;
using MovieRentalSystem.Core.Entities.Customers;
using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Core.Interfaces.Services.Customers;
using MovieRentalSystem.Core.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;
        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerInputModel inputModel, CancellationToken cancellationToken = default)
        {
            inputModel.EnsureIsValid();

            var customerPersisted = await _repository.Query<Customer>().FirstOrDefaultAsync(c => c.Email.Equals(inputModel.Email), cancellationToken);

            if (customerPersisted != null)
                throw new BusinessException("Customer already exists with this email");

            var customer = new Customer(inputModel.Name, inputModel.Email);

            await _repository.AddAsync(customer, cancellationToken);

            return new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }
    }
}
