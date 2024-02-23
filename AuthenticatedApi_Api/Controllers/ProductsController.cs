using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticatedApi_Library;

namespace AuthenticatedApi_Api;
[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ProductsController(AppDataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        return products;
    }

    [HttpGet("ByCategory/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
    {
        var category = new Category { Id = categoryId, Description = String.Empty };
        var productsInCategory = await _context.Products
            .Where(p => p.ProductCategory == category)
            .ToListAsync();

        return productsInCategory;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
