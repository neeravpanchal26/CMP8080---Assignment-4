using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticatedApi_Library;

namespace AuthenticatedApi_Api;
[Authorize]
[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly AppDataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ShoppingCartController(AppDataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> GetShoppingCart()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        var shoppingCart = await _context.ShoppingCarts
            .Include(cart => cart.Products)
            .FirstOrDefaultAsync(cart => cart.User == currentUser);

        return Ok(shoppingCart.Products);
    }

    [HttpPost("RemoveItem")]
    public async Task<ActionResult> RemoveItemFromCart([FromBody] int productId)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        var shoppingCart = await _context.ShoppingCarts
            .Include(cart => cart.Products)
            .FirstOrDefaultAsync(cart => cart.User == currentUser);

        var productToRemove = shoppingCart.Products.FirstOrDefault(p => p.Id == productId);

        if (productToRemove != null)
        {
            shoppingCart.Products.Remove(productToRemove);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("AddItem")]
    public async Task<ActionResult> AddItemToCart([FromBody] int productId)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        var shoppingCart = await _context.ShoppingCarts
            .Include(cart => cart.Products)
            .FirstOrDefaultAsync(cart => cart.User == currentUser);

        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCart
            {
                User = currentUser
            };
            _context.ShoppingCarts.Add(shoppingCart);
        }

        var productToAdd = await _context.Products.FindAsync(productId);
        if (productToAdd == null)
        {
            return NotFound("Product not found.");
        }

        var existingProduct = shoppingCart.Products.FirstOrDefault(p => p.Id == productId);
        if (existingProduct != null)
        {
            return BadRequest("Product already exists in the shopping cart.");
        }

        shoppingCart.Products.Add(productToAdd);
        await _context.SaveChangesAsync();

        return Ok("Product added to the shopping cart successfully.");
    }
}