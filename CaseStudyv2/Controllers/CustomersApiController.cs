using CustomersBusiness;
using CustomersDAL;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServicesHosting;

[ApiController]
[Produces("application/json")]
[Route("api/Customers")]
/// [Authorize]
public class CustomersApiController : ControllerBase, ICustomersApiController
{
    private const int DEFAULT_NO_OF_RECORDS = 25;
    private const string INVALID_BUSINESS_COMPONENT = @"Invalid Business Component Specified!";

    private ICustomersBusinessComponent customersBusinessComponent;

    public CustomersApiController(ICustomersBusinessComponent customersBusinessComponent)
    {
        if (customersBusinessComponent == default(ICustomersBusinessComponent))
            throw new ArgumentException(INVALID_BUSINESS_COMPONENT);

        this.customersBusinessComponent = customersBusinessComponent;
    }

    public void Dispose()
    {
        customersBusinessComponent?.Dispose();
    }

    [HttpGet]
    [Route("detail/{CustomerId}")]
    public async Task<ActionResult<Customer>> GetCustomer(int CustomerId)
    {
        var filteredCustomer = await Task.Run<Customer>(() =>
        {
            return this.customersBusinessComponent.GetCustomerDetail(CustomerId);
        });

        if (filteredCustomer == default(Customer))
            return NotFound();

        return Ok(filteredCustomer);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(
        [FromQuery] int noOfCustomers = DEFAULT_NO_OF_RECORDS
    )
    {
        var Customers = await Task.Run<IEnumerable<Customer>>(() =>
        {
            return this.customersBusinessComponent.GetCustomers().Take(noOfCustomers);
        });

        return Ok(Customers);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> SaveCustomer([FromBody] Customer Customer)
    {
        var validation = Customer != default(Customer);

        if (!validation)
            return BadRequest();

#pragma warning disable CS8604 // Possible null reference argument.
        var status = await Task.Run<bool>(() =>
        {
            return customersBusinessComponent.AddNewCustomer(Customer);
        });
#pragma warning restore CS8604 // Possible null reference argument.

        if (status)
            return Ok(Customer);

        return BadRequest();
    }

    [HttpGet]
    [Route("search/{CustomerName}")]
    public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers(string CustomerName)
    {
        var validation = !string.IsNullOrEmpty(CustomerName);

        if (!validation)
            return BadRequest();

        var filteredCustomers = await Task.Run<IEnumerable<Customer>>(() =>
        {
            return this.customersBusinessComponent.GetCustomers(CustomerName);
        });

        if (filteredCustomers == default(IEnumerable<Customer>))
            return NotFound();

        return Ok(filteredCustomers);
    }
}
