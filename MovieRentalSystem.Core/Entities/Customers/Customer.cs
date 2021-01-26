
namespace MovieRentalSystem.Core.Entities.Customers
{
    public class Customer : EntityBase
    {
        public Customer()
        {
        }

        public Customer(string name, string email) : base()
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
