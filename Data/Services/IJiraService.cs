using BlazorForms.Data.Models.Jira;

namespace BlazorForms.Data.Services;

public interface IJiraService
{
    Task<string?> CreateUserAsync(string email);
    Task<string?> FindUserIdByEmailAsync(string email);
    Task<bool> UserExists(string id);
    Task<IssueResponse?> CreateIssueAsync(Issue issue, ApplicationUser user);
    Task<List<Issue>> GetIssuesByUserEmail(string email);
}