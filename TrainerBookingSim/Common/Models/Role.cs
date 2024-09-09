using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

public class Role : IdentityRole<string>
{
    public ICollection<UserRole> UserRoles { get; set; }
}