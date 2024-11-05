using BlazorForms.Data.Models.Salesforce;

namespace BlazorForms.Data.Services;

public interface ISalesforceService
{
    Task CreateAccount(ApplicationUser user);
    Task<string?> CreateAccount(string userId, SalesforceAccount account);
    Task<Accounts> GetAllAccounts();
    Task<SalesforceAccount?> GetAccountById(string id);

}