using Microsoft.EntityFrameworkCore;

namespace CustomersDAL;

public class CustomersContext : DbContext, ICustomersContext
{
    private const int MIN_ROWS_AFFECTED = 1;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public CustomersContext(DbContextOptions<CustomersContext> dbContextOptions)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        : base(dbContextOptions) { }

    public DbSet<Customer> Customers { get; set; }

    public bool CommitChanges()
    {
        var noOfRowsAffected = this.SaveChanges();

        return noOfRowsAffected >= MIN_ROWS_AFFECTED;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.ApplyConfiguration<Customer>(new CustomerEntityTypeConfiguration());
    }
}
