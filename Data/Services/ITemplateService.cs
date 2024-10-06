using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface ITemplateService
{
    Task<Template> CreateEmptyTemplate();
    Task<List<Template>> GetTemplatesAsync();
    Task<Template?> FindTemplateByIdAsync(Guid id);
    Task UpdateFormNameAsync(Guid templateId, string name);
    Task UpdateDescriptionAsync(Guid templateId, string description);
    Task<IList<Template>> FindTemplatesByAuthor(string authorId);
    Task DeleteTemplateAsync(Guid templateId);
}
