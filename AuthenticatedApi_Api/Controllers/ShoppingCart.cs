using IdentityAPI.Data;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticatedApi_Api
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                .FirstOrDefaultAsync(cart => cart.UserId == currentUser.Id);

            return Ok(shoppingCart.Products);
        }

        [HttpPost("RemoveItem")]
        public async Task<ActionResult> RemoveItemFromCart([FromBody] int productId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == currentUser.Id);

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
    }
}
