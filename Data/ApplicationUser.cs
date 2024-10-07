using BlazorForms.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlazorForms.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public IList<Like> LikedTemplates { get; set; } = new List<Like>();
}