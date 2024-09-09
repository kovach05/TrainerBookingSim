using BusinessLogic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

public class User : IdentityUser<string>
{
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public string ExternalUserId { get; set; }
    
    public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
}