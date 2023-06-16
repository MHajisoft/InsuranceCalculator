using Microsoft.AspNetCore.Identity;

namespace Insurance.Common.Entity;

public class AppUser : IdentityUser<long>
{
    public string Title { get; set; }
}