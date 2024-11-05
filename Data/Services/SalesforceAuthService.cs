using System.Text.Json;
using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public class SalesforceAuthService : ISalesforceAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _username;
    private readonly string _password;
    private readonly string _oAuthUrl;
    
    public string? AccessToken { get; set; }
    public string? InstanceUrl { get; set; }

    public SalesforceAuthService(IConfiguration configuration)
    {
        _clientId = configuration["Salesforce:ClientId"]!;
        _clientSecret = configuration["Salesforce:ClientSecretKey"]!;
        _username = configuration["Salesforce:Username"]!;
        _password = configuration["Salesforce:Password"]!;
        _oAuthUrl = configuration["Salesforce:OauthUrl"]!;
    }

    public async Task<bool> AuthenticateAsync()
    {
        var content = GetEncondedContentToAuthenticate();
        using var client = new HttpClient();

        try
        {
            var response = await client.PostAsync(_oAuthUrl, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var oauthResponse = JsonSerializer.Deserialize<OAuthResponse>(responseBody);

            AccessToken = oauthResponse?.AccessToken;
            InstanceUrl = oauthResponse?.InstanceUrl;

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
            return false;
        }
    }

    public string GetAccessToken()
    {
        return AccessToken ?? string.Empty;
    }


    public bool IsAccessTokenValid() => !string.IsNullOrEmpty(AccessToken);

    private FormUrlEncodedContent? GetEncondedContentToAuthenticate()
    {
        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", _username),
            new KeyValuePair<string, string>("password", _password),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret)
        });
    }
}