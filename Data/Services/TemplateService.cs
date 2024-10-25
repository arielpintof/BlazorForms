﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template = BlazorForms.Data.Models.Template;

namespace BlazorForms.Data.Services;

public class TemplateService : ITemplateService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthenticationStateProvider _authenticationState;
    private readonly UserManager<ApplicationUser> _userManager;

    public TemplateService(ApplicationDbContext context,
        AuthenticationStateProvider authenticationStateProvider, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _authenticationState = authenticationStateProvider;
        _userManager = userManager;
    }

    public async Task<Template> CreateEmptyTemplate()
    {
        var authState = await _authenticationState.GetAuthenticationStateAsync();
        if (authState.User.Identity is { IsAuthenticated: false }) return null;

        var userId = _userManager.GetUserId(authState.User);

        var template = new Template
        {
            Id = Guid.NewGuid(),
            AuthorId = userId!,
            Name = "Untitled form",
            CreatedAt = DateTime.UtcNow
        };

        _context.Templates.Add(template);
        await _context.SaveChangesAsync();
        //await _elasticClient.IndexDocumentAsync(template);
        return template;
    }

    public async Task<Template?> FindTemplateByIdAsync(Guid id)
    {
        return await _context.Templates
            .Include(t => t.Fields)
            .ThenInclude(e => e.Options)
            .Include(t => t.Author)
            .Include(t => t.Comments)
            .Include(t => t.Responses)
            .ThenInclude(e => e.FieldResponses)
            .Include(t => t.Responses)
            .ThenInclude(e => e.Responder)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Template>> GetTemplatesAsync()
    {
        return await _context.Templates
            .Include(t => t.Fields)
            .ThenInclude(e => e.Options)
            .Include(t => t.Author)
            .Include(t => t.Comments)
            .Include(e => e.Tags)
            .ToListAsync();
    }

    public async Task UpdateFormNameAsync(Guid templateId, string name)
    {
        await _context.Templates
            .Where(t => t.Id == templateId)
            .ExecuteUpdateAsync(e => e.SetProperty(t => t.Name, name));
    }

    public async Task UpdateDescriptionAsync(Guid templateId, string description)
    {
        await _context.Templates
            .Where(t => t.Id == templateId)
            .ExecuteUpdateAsync(e => e.SetProperty(t => t.Description, description));
    }

    public async Task<IList<Template>> FindTemplatesByAuthor(string authorId)
    {
        return await _context.Templates.Where(e => e.AuthorId == authorId).ToListAsync();
    }

    public async Task DeleteTemplateAsync(Guid templateId)
    {
        //await _context.Templates.Where(e => e.Id == templateId).ExecuteDeleteAsync();
        var template = await _context.Templates.SingleOrDefaultAsync(e => e.Id == templateId);
        if (template is null) return;

        _context.Templates.Remove(template);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateImageUrl(Guid templateId, string imageUrl)
    {
        var template = await _context.Templates.SingleOrDefaultAsync(e => e.Id == templateId);
        if (template is null) return;

        template.ImageUrl = imageUrl;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTemplate(Template updatedTemplate)
    {
        var template = await _context.Templates.SingleOrDefaultAsync(e => e.Id == updatedTemplate.Id);
        if (template is null) return;

        _context.Entry(template).CurrentValues.SetValues(updatedTemplate);
        await _context.SaveChangesAsync();
    }

    public async Task AddAllowedUser(ApplicationUser applicationUser, Guid templateId)
    {
        var template = await _context.Templates.SingleOrDefaultAsync(e => e.Id == templateId);
        Console.WriteLine(applicationUser.Id);
        if (template is null) return;
        template.AllowedResponderIds.Add(applicationUser.Id);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAllowedUser(ApplicationUser applicationUser, Guid templateId)
    {
        var template = await _context.Templates.SingleOrDefaultAsync(e => e.Id == templateId);
        template?.AllowedResponderIds.Remove(applicationUser.Id);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Template>> SearchTemplatesAsync(string searchTerm)
    {
        searchTerm = searchTerm.ToLower();

        var templates = await _context.Templates
            .Where(t =>
                (t.Name != null && t.Name.ToLower().Contains(searchTerm)) ||
                (t.Description != null && t.Description.ToLower().Contains(searchTerm)) ||
                t.Fields.Any(f =>
                    (f.Title != null && f.Title.ToLower().Contains(searchTerm)) ||
                    (f.Description != null && f.Description.ToLower().Contains(searchTerm))
                ) ||
                t.Comments.Any(c =>
                    c.Message.ToLower().Contains(searchTerm)
                ) ||
                t.Tags.Any(e => e.Name.ToLower().Contains(searchTerm))
            )
            .Include(e => e.Comments)
            .Include(t => t.Fields)
            .ToListAsync();

        return templates;
    }

    public async Task<List<Template>> FindTemplatesByTagAsync(string tag)
    {
        return await _context.Templates.Where(e => e.Tags.Any(t => t.Name.Contains(tag))).ToListAsync();
    }

    public async Task<List<Template>> GetMostPopularTemplatesAsync(int number)
    {
        return await _context.Templates.Where(e => e.IsPublic == true)
            .OrderByDescending(e => e.Responses.Count).Take(number).ToListAsync();
    }
}