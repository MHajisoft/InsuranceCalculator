using Microsoft.AspNetCore.Identity;

namespace Insurance.Common.Entity;

public class AppRole : IdentityRole<long>
{
    public string Title { get; set; }
}