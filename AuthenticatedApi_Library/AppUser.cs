using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Models;

public class AppUser : IdentityUser
{
    public DateTime DOB { get; set; }
    
    public string NickName { get; set; } = String.Empty;
}