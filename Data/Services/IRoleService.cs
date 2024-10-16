using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Data.Services;

public interface IRoleService
{
    Task Create([Required] string name);
    Task AssignRoleToUser(string userId, string roleName);
}