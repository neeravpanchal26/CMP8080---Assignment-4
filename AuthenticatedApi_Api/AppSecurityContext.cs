using AuthenticatedApi_Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatedApi_Api;

public class AppSecurityContext : IdentityDbContext<AppUser>
{
    public AppSecurityContext(DbContextOptions<AppSecurityContext> options)
        : base(options)
        {}
}