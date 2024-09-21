using Microsoft.EntityFrameworkCore;
using OA.Domain.Entities;

namespace OA.Persistence;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Supplier> Suppliers { get; set; }

    Task<int> SaveChangesAsync();
}