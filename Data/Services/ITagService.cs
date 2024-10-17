using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface ITagService
{
    Task<List<Tag>> FindAllTags();
    Task AddTagToTemplateAsync(Guid templateId, string tagName);
    Task AddTagsToTemplateAsync(Guid templateId, IList<string> tagNames);
}