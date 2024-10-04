using BlazorForms.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;

public class FieldService : IFieldService
{
    private readonly ApplicationDbContext _context;

    public FieldService(ApplicationDbContext context)
    {
        _context = context;
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

    public async Task<Field?> FindFieldByIdASync(Guid id) => 
        await _context.Fields
            .Include(e => e.Options)
            .FirstOrDefaultAsync(f => f.Id == id);

    public async Task UpdateFieldAsync(Field updatedField)
    {
        var field = await _context.Fields.SingleOrDefaultAsync(e => e.Id == updatedField.Id);
        if (field is null) return;
        
        _context.Entry(field).CurrentValues.SetValues(updatedField);
        await _context.SaveChangesAsync();
        //await _context.Fields.ExecuteUpdateAsync(f => f.SetProperty(p => p, field));
    }

    public async Task AddQuestion(Guid fieldId)
    {
        try
        {
            // Verificar si el field existe y cargarlo con sus opciones
            var field = await _context.Fields
                .Include(f => f.Options)
                .SingleOrDefaultAsync(f => f.Id == fieldId);

            if (field == null)
            {
                // Manejar el caso donde el campo no exista
                Console.WriteLine("El campo no existe.");
                return;
            }

            // Agregar la nueva opción
            var option = new Option
            {
                Id = Guid.NewGuid(),
                Value = $"Option {field.Options.Count + 1}",
                FieldId = fieldId
            };
            
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Manejar el conflicto de concurrencia
            Console.WriteLine("Conflicto de concurrencia detectado. Detalles: " + ex.Message);
            // Aquí puedes decidir si vuelves a cargar la entidad y reintentas la operación
        }
        catch (Exception ex)
        {
            // Manejar cualquier otro error que pueda ocurrir
            Console.WriteLine("Error al agregar la pregunta: " + ex.Message);
        }
    }

    public async Task RemoveQuestion(Guid optionId)
    {
        var option = await _context.Options.SingleOrDefaultAsync(e => e.Id == optionId);
        if (option is null) return;

        _context.Options.Remove(option);
        await _context.SaveChangesAsync();
    }
}