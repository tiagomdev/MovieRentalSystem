using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.Core.Application.InputModels.Customers;
using MovieRentalSystem.Core.Interfaces.Services.Customers;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerInputModel inputModel, 
            CancellationToken cancellationToken = default)
        {
            var customerCreated = await _customerService.CreateCustomerAsync(inputModel, cancellationToken);

            return Created($"api/customers/{customerCreated.Id}", customerCreated);
        }
    }
}
