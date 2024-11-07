using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorForms.Data.Models.Jira;
using Microsoft.AspNetCore.Identity;

namespace BlazorForms.Data.Services;

public class JiraService : IJiraService
{
    private readonly string _username;
    private readonly string _password;
    private readonly string _projectKey;
    private UserManager<ApplicationUser> _userManager;
    private const string BasePath = "https://blazorform.atlassian.net/rest/api/2";

    public JiraService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _password = configuration["Jira:Password"]!;
        _username = configuration["Jira:Username"]!;
        _projectKey = "SOP";
        _userManager = userManager;
    }

    public async Task<string?> CreateUserAsync(string email)
    {
        const string url = $"{BasePath}/user/";
        using var client = new HttpClient();
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        var body = JsonSerializer.Serialize(new
        {
            emailAddress = email,
            products = new List<string>()
        });
        var stringContent = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<UserCreatedResponse>(content);
        
        return result?.accountId;
    }

    public async Task<string?> FindUserIdByEmailAsync(string email)
    {
        var url = $"{BasePath}/user/search?query={email}";
        using var client = new HttpClient();
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;
        
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<UserCreatedResponse>(content);

        return result?.accountId;
    }

    public async Task<bool> UserExists(string email)
    {
        var url = $"{BasePath}/user/search?query={email}";
        using var client = new HttpClient();
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        var response = await client.GetAsync(url);

        return response.IsSuccessStatusCode;
    }

    public async Task<IssueResponse?> CreateIssueAsync(Issue issue, ApplicationUser user)
    {
        if (user.Email is null) return null;
        
        if (!await UserExists(user.Email))
        {
            var jiraUserId = await CreateUserAsync(user.Email);
            await AddJiraIdToUser(jiraUserId, user);
        }

        const string url = $"{BasePath}/issue";
        using var client = new HttpClient();
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        var body = JsonSerializer.Serialize(issue);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var issueResponse = JsonSerializer.Deserialize<IssueResponse>(responseContent);
        return issueResponse;
    }

    private async Task AddJiraIdToUser(string? jiraUserId, ApplicationUser user)
    {
        if (jiraUserId is null) throw new Exception();
        user.JiraAccountId = jiraUserId;
        
        await _userManager.UpdateAsync(user);
    }
}

public class IssueResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("key")]
    public string Key { get; set; }
    [JsonPropertyName("self")]
    public string Self { get; set; }
}