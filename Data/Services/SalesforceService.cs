using System.Net.Http.Headers;
using System.Text;
using BlazorForms.Data.Models.Salesforce;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;

public class SalesforceService : ISalesforceService
{
    private readonly ISalesforceAuthService _salesforceAuthService;
    private readonly UserManager<ApplicationUser> _userManager;


    public SalesforceService(ISalesforceAuthService salesforceAuthService, UserManager<ApplicationUser> userManager)
    {
        _salesforceAuthService = salesforceAuthService;
        _userManager = userManager;
    }

    public Task CreateAccount(ApplicationUser user)
    {
        var userExists = _userManager.Users.Any(e => e.Equals(user));
        return Task.CompletedTask;
    }

    public async Task<string?> CreateAccount(string userId, SalesforceAccount account)
    {
        Console.WriteLine("CreateAccount");
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == userId);
        if (user is null)
        {
            Console.WriteLine("null user");
            return null;
        }

        if (user.SalesforceAccountId is not null) return null;
        
        
        await _salesforceAuthService.AuthenticateAsync();

        var url = $"{_salesforceAuthService.InstanceUrl}/services/data/v62.0/sobjects/Account";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _salesforceAuthService.AccessToken);

        var content = JsonSerializer.Serialize(account);
        var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("PETICION NULA");
            return null;
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

        if (result != null && result.TryGetValue("id", out var accountId))
        {
            Console.WriteLine(accountId.ToString());
            return accountId.ToString();
        }

        return null;
    }

    public async Task<Accounts> GetAllAccounts()
    {
        if (!_salesforceAuthService.IsAccessTokenValid())
        {
            await _salesforceAuthService.AuthenticateAsync();
        }

        const string query = "SELECT Name FROM Account";
        var url = $"{_salesforceAuthService.InstanceUrl}/services/data/v62.0/query/?q={Uri.EscapeDataString(query)}";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _salesforceAuthService.AccessToken);

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<Accounts>(responseBody);
            if (accounts is not null) return accounts;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error fetching accounts: " + e.Message);
        }

        return new Accounts();
    }

    public async Task<SalesforceAccount?> GetAccountById(string id)
    {
        if (!_salesforceAuthService.IsAccessTokenValid())
        {
            await _salesforceAuthService.AuthenticateAsync();
        }

        var query = $"SELECT Name, ShippingCountry FROM Account WHERE Id = '{id}'";
        var url = $"{_salesforceAuthService.InstanceUrl}/services/data/v62.0/query/?q={Uri.EscapeDataString(query)}";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _salesforceAuthService.AccessToken);

        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return new SalesforceAccount();
        var responseBody = await response.Content.ReadAsStringAsync();
        var queryResult = JsonSerializer.Deserialize<Accounts>(responseBody);

        if (queryResult == null || queryResult.Records.Count <= 0) return null;

        return new SalesforceAccount
        {
            Name = queryResult.Records[0].Name,
            ShippingCountry = queryResult.Records[0].ShippingCountry,
        };
    }
}

public class QueryResult<T>
{
    public List<T> Records { get; set; } = [];
}