using Microsoft.EntityFrameworkCore;
using AuthenticatedApi_Library;

namespace AuthenticatedApi_Api;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options)
        {}

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
}