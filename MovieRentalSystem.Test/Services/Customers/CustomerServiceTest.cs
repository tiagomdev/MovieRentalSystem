using Moq;
using MovieRentalSystem.Core.Application.InputModels.Customers;
using MovieRentalSystem.Core.Application.Services.Customers;
using MovieRentalSystem.Core.Entities.Customers;
using MovieRentalSystem.Core.Exceptions;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Infra.Data.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieRentalSystem.Test.Services.Customers
{
    class CustomerServiceTest : TestBase
    {
        [Test]
        public async Task CreateCustomerWithSuccess()
        {
            using (var context = CreateContext())
            {
                var customerService = new CustomerService(new RepositoryBase(context));

                var inputModel = new CreateCustomerInputModel() { Name = "Test", Email = "test@hotmail.com" };

                var result = await customerService.CreateCustomerAsync(inputModel);

                Assert.AreEqual(inputModel.Name, result.Name);
                Assert.AreEqual(inputModel.Email, result.Email);
                Assert.IsTrue(result.Id != default);

                ClearContext(context);
            }
        }

        [Test]
        public async Task CatchCreateCustomerWithExistingEmail()
        {
            using (var context = CreateContext())
            {
                var inputModel = new CreateCustomerInputModel() { Name = "Test", Email = "test@hotmail.com" };

                await context.AddAsync(new Customer() { Email = inputModel.Email });
                await context.SaveChangesAsync();

                var customerService = new CustomerService(new RepositoryBase(context));

                Assert.ThrowsAsync<BusinessException>(async () => await customerService.CreateCustomerAsync(inputModel));

                ClearContext(context);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CatchCreateCustomerWithInvalidName(string name)
        {
            var customerService = new CustomerService(Mock.Of<IRepository>());

            var inputModel = new CreateCustomerInputModel() { Name = name };

            Assert.ThrowsAsync<ModelValidationException>(async () => await customerService.CreateCustomerAsync(inputModel));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("test")]
        public void CatchCreateCustomerWithInvalidEmail(string email)
        {
            var customerService = new CustomerService(Mock.Of<IRepository>());

            var inputModel = new CreateCustomerInputModel() { Name = "test", Email = email };

            Assert.ThrowsAsync<ModelValidationException>(async () => await customerService.CreateCustomerAsync(inputModel));
        }
    }
}
