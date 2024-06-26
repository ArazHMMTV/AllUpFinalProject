using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser:IdentityUser
{
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
}
