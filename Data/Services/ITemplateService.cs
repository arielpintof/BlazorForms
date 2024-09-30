using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface ITemplateService
{
    Task<Template> CreateEmptyForm();
    Task<List<Template>> GetTemplatesAsync();
    Task<Template?> FindTemplateByIdAsync(Guid id);
}