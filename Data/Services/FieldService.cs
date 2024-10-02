using BlazorForms.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;

public class FieldService : IFieldService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthenticationStateProvider _authenticationState;
    private readonly UserManager<ApplicationUser> _userManager;

    public FieldService(ApplicationDbContext context,
        AuthenticationStateProvider authenticationStateProvider, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _authenticationState = authenticationStateProvider;
        _userManager = userManager;
    }

    public async Task<Field> AddFieldToTemplateAsync(Guid templateId, FieldType type = FieldType.MultiSelection,
        bool showInTable = false)
    {
        var fields = await _context.Fields
            .Where(f => f.TemplateId == templateId)
            .ToListAsync();

        var maxOrder = fields.Count != 0 ? fields.Max(f => f.Order) : 0;

        var newField = new Field
        {
            Id = Guid.NewGuid(),
            Order = maxOrder + 1,
            TemplateId = templateId,
            Type = type
        };

        /*var template = await _context.Templates
            .Include(t => t.Fields)
            .Include(t => t.Author)
            .FirstOrDefaultAsync(t => t.Id == templateId);
        
        _context.Attach(template);*/

        await _context.Fields.AddAsync(newField);
        await _context.SaveChangesAsync();

        return newField;
    }

    public async Task<List<Field>> FindFieldsForTemplateAsync(Guid templateId)
    {
        return await _context.Fields
            .Where(f => f.TemplateId == templateId)
            .Include(f => f.Options)
            .ToListAsync();
    }

    //public async Task DeleteFieldAsync(Guid id) => await _context.Fields.Where(f => f.Id == id).ExecuteDeleteAsync();
    public async Task DeleteFieldAsync(Guid id)
    {
        var field = await _context.Fields.FirstOrDefaultAsync(f => f.Id == id);
        if (field is null) return;
        
        _context.Remove(field);
        await _context.SaveChangesAsync();
    }

    public async Task<Field?> FindFieldByIdASync(Guid id) => await _context.Fields.FirstOrDefaultAsync(f => f.Id == id);
    
    public async Task UpdateFieldAsync(Field field)
    {
        await _context.Fields.ExecuteUpdateAsync(f => f.SetProperty(p => p, field));
        
    }
}