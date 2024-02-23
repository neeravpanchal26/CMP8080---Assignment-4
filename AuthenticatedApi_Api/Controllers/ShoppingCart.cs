using IdentityAPI.Data;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace AuthenticatedApi_Api;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    public ShoppingCartController()
    {
    }
}
