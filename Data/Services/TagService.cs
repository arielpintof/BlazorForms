using BlazorForms.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;

public class TagService : ITagService
{
    private readonly ApplicationDbContext _context;

    public TagService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tag>> FindAllTags() => await _context.Tags.ToListAsync();

    public Task AddTagToTemplateAsync(Guid templateId, string tagName)
    {
        throw new NotImplementedException();
    }

    public async Task AddTagsToTemplateAsync(Guid templateId, IList<string> tagNames)
    {
        var template = await _context.Templates.Include(t => t.Tags)
            .FirstOrDefaultAsync(e => e.Id == templateId);

        if (template is null)
        {
            Console.WriteLine("null template");
            return;
        }

        var currentTags = template.Tags.Select(e => e.Name).ToList();
        var newTags = tagNames.Except(currentTags).ToList();
        var tagsToRemove = currentTags.Except(tagNames).ToList();

        foreach (var newTagName in newTags)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name.ToLower().Equals(newTagName.ToLower()));

            if (tag is null)
            {
                tag = new Tag
                {
                    Name = newTagName
                };
                
                _context.Tags.Add(tag);
            }

            template.Tags.Add(tag);
        }

        foreach (var tagName in tagsToRemove)
        {
            var tagToRemove = template.Tags.FirstOrDefault(t => t.Name == tagName);
            if (tagToRemove != null)
            {
                template.Tags.Remove(tagToRemove);
            }
        }

        await _context.SaveChangesAsync();
    }
}