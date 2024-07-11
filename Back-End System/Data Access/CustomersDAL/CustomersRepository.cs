namespace CustomersDAL;

public class CustomersRepository : ICustomersRepository
{
    private const string INVALID_CustomerS_CONTEXT = @"Invalid Customer Context Specified!";
    private const string INVALID_ARGUMENTS = @"Invalid Argument(s) Specified!";

    private ICustomersContext customersContext;

    public CustomersRepository(ICustomersContext customersContext)
    {
        if (customersContext == default(ICustomersContext))
            throw new ArgumentException(INVALID_CustomerS_CONTEXT);

        this.customersContext = customersContext;
    }

    public bool AddEntity(Customer entityObject)
    {
        var status = default(bool);
        var validation = entityObject != default(Customer);

        if (!validation)
            throw new ArgumentException(INVALID_ARGUMENTS);

#pragma warning disable CS8604 // Possible null reference argument.
        _ = this.customersContext.Customers.Add(entityObject);
#pragma warning restore CS8604 // Possible null reference argument.
        status = this.customersContext.CommitChanges();

        return status;
    }

    public void Dispose() => this.customersContext?.Dispose();

    public IEnumerable<Customer> GetEntities()
    {
        var validation = this.customersContext != default(ICustomersContext);

        if (!validation)
            throw new ArgumentException(INVALID_CustomerS_CONTEXT);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var Customers = customersContext.Customers.ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return Customers;
    }

    public Customer GetEntityByKey(int entityKey)
    {
        var validation = entityKey != default(int);

        if (!validation)
            throw new ArgumentException(INVALID_ARGUMENTS);

        var filteredCustomer = this
            .customersContext.Customers.Where(Customer => Customer.CustomerId.Equals(entityKey))
            .FirstOrDefault();

#pragma warning disable CS8603 // Possible null reference return.
        return filteredCustomer;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public IEnumerable<Customer> GetCustomersByName(string CustomerName)
    {
        var validation = !string.IsNullOrEmpty(CustomerName);

        if (!validation)
            throw new ArgumentException(INVALID_ARGUMENTS);

        var filteredProdcuts = this
            .customersContext.Customers.Where(Customer =>
                Customer.CustomerName.Contains(CustomerName)
            )
            .ToList();

        return filteredProdcuts;
    }
}
