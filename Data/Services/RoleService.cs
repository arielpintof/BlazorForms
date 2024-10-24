using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task Create(string name)
    {
        var roleExists = await _roleManager.RoleExistsAsync("admin");
        if (!roleExists)
        {
            var role = await _roleManager.CreateAsync(new IdentityRole("admin"));
            if (!role.Succeeded) throw new Exception();
        }
    }

    public async Task AssignRoleToUser(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded) throw new Exception("Could not assign role to user");
    }

    public async Task<bool> IsAdmin(ApplicationUser user)
    {
        var adminRole = await _roleManager.FindByNameAsync("admin");
        if (adminRole?.Name is null) return false;

        return await _userManager.IsInRoleAsync(user, adminRole.Name);
    }

    public async Task RemoveRoleToUser(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded) throw new Exception("Could not assign role to user");
    }

    public async Task<bool> IsAdmin(string userId)
    {
        var adminRole = await _roleManager.FindByNameAsync("admin");
        if (adminRole?.Name is null) return false;
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return false;

        var isAdmin = _context.UserRoles
            .Any(e => e.UserId == userId && e.RoleId == adminRole.Id);
        
        return isAdmin;
    }
}