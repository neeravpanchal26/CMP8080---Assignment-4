using Microsoft.AspNetCore.Identity;

namespace AuthenticatedApi_Library;

public class AppUser : IdentityUser
{
    public DateTime DOB { get; set; }
    
    public string NickName { get; set; } = String.Empty;
}