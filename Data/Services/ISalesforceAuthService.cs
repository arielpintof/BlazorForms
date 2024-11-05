namespace BlazorForms.Data.Services;

public interface ISalesforceAuthService
{
    public string? AccessToken { get; set; }
    public string? InstanceUrl { get; set; }
    Task<bool> AuthenticateAsync();
    string GetAccessToken();
    bool IsAccessTokenValid();
    
    
}