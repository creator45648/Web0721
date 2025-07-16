using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Web.View.Models;
namespace Web.View.Pages;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        group.MapGet("/", async (NorthwindContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (string customerid, NorthwindContext db) =>
        {
            return await db.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CustomerId == customerid)
                is Customer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string customerid, Customer customer, NorthwindContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.CustomerId == customerid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.CustomerId, customer.CustomerId)
                    .SetProperty(m => m.CompanyName, customer.CompanyName)
                    .SetProperty(m => m.ContactName, customer.ContactName)
                    .SetProperty(m => m.ContactTitle, customer.ContactTitle)
                    .SetProperty(m => m.Address, customer.Address)
                    .SetProperty(m => m.City, customer.City)
                    .SetProperty(m => m.Region, customer.Region)
                    .SetProperty(m => m.PostalCode, customer.PostalCode)
                    .SetProperty(m => m.Country, customer.Country)
                    .SetProperty(m => m.Phone, customer.Phone)
                    .SetProperty(m => m.Fax, customer.Fax)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer customer, NorthwindContext db) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Customer/{customer.CustomerId}",customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string customerid, NorthwindContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.CustomerId == customerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
