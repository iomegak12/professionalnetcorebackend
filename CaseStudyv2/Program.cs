using System.Text;
using CustomersBusiness;
using CustomersDAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string CONNECTION_STRING_KEY = "CUSTOMERS_DB_CONNECTION_STRING";

builder.Services.AddDbContext<CustomersContext>(dbContextOptionsBuilder =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    var decodedConnectionString = Encoding.ASCII.GetString(
        Convert.FromBase64String(Environment.GetEnvironmentVariable(CONNECTION_STRING_KEY))
    );
#pragma warning restore CS8604 // Possible null reference argument.

    _ = dbContextOptionsBuilder.UseSqlServer(decodedConnectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<ICustomersContext, CustomersContext>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IBusinessValidator<string>, CustomerSearchStringValidator>();
builder.Services.AddScoped<IBusinessValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<ICustomersBusinessComponent, CustomersBusinessComponent>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
