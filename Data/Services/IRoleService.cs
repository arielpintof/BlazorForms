using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Data.Services;

public interface IRoleService
{
    /// <summary>
    /// Creates a new role if it does not already exist.
    /// </summary>
    /// <param name="name">The name of the role to create. This parameter is required.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Create([Required] string name);
    
    /// <summary>
    /// Assigns a specified role to a user.
    /// </summary>
    /// <param name="userId">The ID of the user to whom the role will be assigned.</param>
    /// <param name="roleName">The name of the role to assign.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AssignRoleToUser(string userId, string roleName);
    
    /// <summary>
    /// Determines whether a user with the specified user ID is in the "admin" role.
    /// </summary>
    /// <param name="userId">The ID of the user to check.</param>
    /// <returns>A task that returns true if the user is in the "admin" role; otherwise, false.</returns>
    Task<bool> IsAdmin(string userId);
    
    // <summary>
    /// Determines whether the specified user is in the "admin" role.
    /// </summary>
    /// <param name="applicationUser">The user object to check.</param>
    /// <returns>A task that returns true if the user is in the "admin" role; otherwise, false.</returns>
    Task<bool> IsAdmin(ApplicationUser applicationUser);

    Task RemoveRoleToUser(string userId, string roleName);
}