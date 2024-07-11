using CustomersDAL;

namespace CustomersBusiness;

public interface ICustomersBusinessComponent : IDisposable
{
    IEnumerable<Customer> GetCustomers(string? CustomerName = default);
    Customer GetCustomerDetail(int CustomerId);
    bool AddNewCustomer(Customer Customer);
}
