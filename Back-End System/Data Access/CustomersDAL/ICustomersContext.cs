using Microsoft.EntityFrameworkCore;

namespace CustomersDAL;

public interface ICustomersContext : ISystemContext
{
    DbSet<Customer> Customers { get; set; }
}
