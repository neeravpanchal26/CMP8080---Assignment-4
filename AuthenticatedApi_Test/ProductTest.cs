using AuthenticatedApi_Library;
using AuthenticatedApi_Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticatedApi_Api_Tests;

[TestClass]
public class ProductsTests
{
    private static DbContextOptions<AppDataContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        return new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInternalServiceProvider(serviceProvider)
            .Options;
    }

    [TestMethod]
    public async Task GetProducts_ReturnsListOfProducts()
    {
        
        var options = CreateNewContextOptions();
        using (var context = new AppDataContext(options))
        {
            var category = new Category { Id = 1, Description = "Test Category" };
            var product1 = new Product { Id = 1, Name = "Test Product 1", Price = 10.99m, Description = "Test Description 1", ProductCategory = category };
            var product2 = new Product { Id = 2, Name = "Test Product 2", Price = 20m, Description = "Test Description 2", ProductCategory = category };
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.SaveChanges();
        }

        using (var context = new AppDataContext(options))
        {
            var controller = new ProductsController(context, null);

            
            var result = await controller.GetProducts();

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Product>));
            Assert.AreEqual(2, result.Value.Count());
        }
    }

    [TestMethod]
    public async Task GetProductsByCategory_ReturnsProductsInCategory()
    {
        
        var options = CreateNewContextOptions();
        using (var context = new AppDataContext(options))
        {
            var category = new Category { Id = 1, Description = "Category 1" };
            context.Products.Add(new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Test Description 1", ProductCategory = category });
            context.SaveChanges();
        }

        using (var context = new AppDataContext(options))
        {
            var controller = new ProductsController(context, null);

            
            var result = await controller.GetProductsByCategory(1);

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Product>));
            Assert.AreEqual(1, result.Value.Count());
            Assert.AreEqual("Product 1", result.Value.First().Name);
        }
    }

    [TestMethod]
    public async Task CreateProduct_ReturnsOkResult()
    {
        
        var options = CreateNewContextOptions();
        using (var context = new AppDataContext(options))
        {
            var controller = new ProductsController(context, null);
            var category = new Category { Id = 1, Description = "Category 1" };
            var newProduct = new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Test Description 1", ProductCategory = category };

            
            var result = await controller.CreateProduct(newProduct);

            
            Assert.IsInstanceOfType(result, typeof(ActionResult<Product>));
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }
    }

    [TestMethod]
    public async Task CreateProduct_AddsProductToDatabase()
    {
        
        var options = CreateNewContextOptions();
        using (var context = new AppDataContext(options))
        {
            var controller = new ProductsController(context, null);
            var category = new Category { Id = 1, Description = "Category 1" };
            var newProduct = new Product { Id = 1, Name = "New Product", Price = 15, Description = "Test Description 1", ProductCategory = category };

            
            await controller.CreateProduct(newProduct);
        }

        
        using (var context = new AppDataContext(options))
        {
            Assert.AreEqual(1, context.Products.Count());
            var product = context.Products.First();
            Assert.AreEqual("New Product", product.Name);
            Assert.AreEqual(15, product.Price);
        }
    }
}
