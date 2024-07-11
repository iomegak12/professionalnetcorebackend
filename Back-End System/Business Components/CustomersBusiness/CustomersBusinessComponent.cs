using CustomersDAL;

namespace CustomersBusiness;

public class CustomersBusinessComponent : ICustomersBusinessComponent
{
    private const string INVALID_CustomerS_REPOSITORY =
        @"Invalid Customer(s) Repository Specified!";
    private const string BUSINESS_VALIDATION_FAILED = @"Customers Business Validation Failed!";
    private const string INVALID_ARGUMENTS = @"Invalid Argument(s) Specified!";

    private ICustomersRepository CustomersRepository;
    private IBusinessValidator<string> CustomerNameValidator;
    private IBusinessValidator<Customer> CustomerValidator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public CustomersBusinessComponent(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        ICustomersRepository CustomersRepository,
        IBusinessValidator<string> CustomerNameValidator,
        IBusinessValidator<Customer> CustomerValidator
    )
    {
        if (CustomersRepository == default(ICustomersRepository))
            throw new ArgumentException(INVALID_CustomerS_REPOSITORY);

        if (
            CustomerNameValidator == default(IBusinessValidator<string>)
            && CustomerValidator == default(IBusinessValidator<Customer>)
        )
            throw new ArgumentException(INVALID_ARGUMENTS);

        this.CustomersRepository = CustomersRepository;
#pragma warning disable CS8601 // Possible null reference assignment.
        this.CustomerNameValidator = CustomerNameValidator;
#pragma warning restore CS8601 // Possible null reference assignment.
        this.CustomerValidator = CustomerValidator;
    }

    public bool AddNewCustomer(Customer Customer)
    {
        var validation = Customer != default(Customer) && this.CustomerValidator.Validate(Customer);

        if (!validation)
            throw new ApplicationException(BUSINESS_VALIDATION_FAILED);

#pragma warning disable CS8604 // Possible null reference argument.
        var status = this.CustomersRepository.AddEntity(Customer);
#pragma warning restore CS8604 // Possible null reference argument.

        return status;
    }

    public void Dispose() => this.CustomersRepository?.Dispose();

    public Customer GetCustomerDetail(int CustomerId)
    {
        var validation = CustomerId != default(int);

        if (!validation)
            throw new ArgumentException(INVALID_ARGUMENTS);

        var filteredCustomer = this.CustomersRepository.GetEntityByKey(CustomerId);

        return filteredCustomer;
    }

    public IEnumerable<Customer> GetCustomers(string? CustomerName = default)
    {
        var Customers = default(IEnumerable<Customer>);

        if (string.IsNullOrEmpty(CustomerName))
            Customers = this.CustomersRepository.GetEntities();
        else
        {
            var businessValidation = this.CustomerNameValidator.Validate(CustomerName);

            if (!businessValidation)
                throw new ApplicationException(BUSINESS_VALIDATION_FAILED);

            Customers = this.CustomersRepository.GetCustomersByName(CustomerName);
        }

        return Customers;
    }
}
