using AuthenticatedApi_Library;

namespace AuthenticatedApi_Test;
[TestClass]
public class ShoppingCartTests
{
    [TestMethod]
    public void ShoppingCart_ConstructsCorrectly()
    {   
        var user = new AppUser();
        var products = new List<Product>();
        
        var shoppingCart = new ShoppingCart
        {
            Id = 1,
            User = user,
            Products = products
        };
        
        Assert.IsNotNull(shoppingCart);
        Assert.AreEqual(1, shoppingCart.Id);
        Assert.AreSame(user, shoppingCart.User);
        Assert.AreSame(products, shoppingCart.Products);
    }

    [TestMethod]
    public void ShoppingCart_DefaultConstructor_SetsProductsToEmptyList()
    {
        var shoppingCart = new ShoppingCart();
        
        Assert.IsNotNull(shoppingCart.Products);
        Assert.AreEqual(0, shoppingCart.Products.Count);
    }

    [TestMethod]
    public void ShoppingCart_SetUser()
    {
        var shoppingCart = new ShoppingCart();
        var user = new AppUser();

        shoppingCart.User = user;
        
        Assert.AreSame(user, shoppingCart.User);
    }

    [TestMethod]
    public void ShoppingCart_AddProduct()
    {
        var user = new AppUser();

        var category = new Category { Id = 1, Description = "Test Category" };
        var product = new Product { Id = 1, Name = "Test Product 1", Price = 10.99m, Description = "Test Description 1", ProductCategory = category };
        var products = new List<Product>();
        products.Add(product);

        var shoppingCart = new ShoppingCart
        {
            Id = 1,
            User = user,
            Products = products
        };
        
        Assert.AreEqual(1, shoppingCart.Products.Count);
        Assert.AreSame(product, shoppingCart.Products[0]);
    }
}
