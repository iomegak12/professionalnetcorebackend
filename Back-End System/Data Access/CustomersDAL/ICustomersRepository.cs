namespace CustomersDAL;

public interface ICustomersRepository : IRepository<Customer, int>
{
    IEnumerable<Customer> GetCustomersByName(string CustomerName);
}
