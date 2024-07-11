using CustomersDAL;

namespace CustomersBusiness;

public class CustomerValidator : IBusinessValidator<Customer>
{
    private const int MIN_CREDIT_LIMIT = 1;

    public bool Validate(Customer tObject)
    {
        var validation =
            tObject != default(Customer)
            && !string.IsNullOrEmpty(tObject.CustomerName)
            && tObject.CreditLimit >= MIN_CREDIT_LIMIT
            && tObject.IsActive;

        return validation;
    }
}
