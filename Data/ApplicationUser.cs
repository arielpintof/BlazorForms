using BlazorForms.Data.Models;
using BlazorForms.Data.Services;
using Microsoft.AspNetCore.Identity;

namespace BlazorForms.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public IList<Like> LikedTemplates { get; set; } = new List<Like>();
    public string? SalesforceAccountId { get; set; }
}

public static class ApplicationUserExtension
{
}