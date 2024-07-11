using CustomersDAL;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServicesHosting;

public interface ICustomersApiController : IDisposable
{
    Task<ActionResult<IEnumerable<Customer>>> GetCustomers(int noOfRecords = default(int));
    Task<ActionResult<IEnumerable<Customer>>> SearchCustomers(string CustomerName);
    Task<ActionResult<Customer>> GetCustomer(int CustomerId);
    Task<ActionResult<Customer>> SaveCustomer(Customer Customer);
}
