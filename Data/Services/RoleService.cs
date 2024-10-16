using Microsoft.AspNetCore.Identity;

namespace BlazorForms.Data.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task Create(string name)
    {
        var roleExists = await _roleManager.RoleExistsAsync("admin");
        if (!roleExists)
        {
            var role =await _roleManager.CreateAsync(new IdentityRole("admin"));
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
}