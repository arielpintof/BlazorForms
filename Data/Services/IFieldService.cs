using BlazorForms.Data.Enums;
using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface IFieldService
{
    Task<Field> AddFieldToTemplateAsync(Guid templateId, FieldType type = FieldType.MultiSelection,
        bool showInTable = false);
    Task<List<Field>> FindFieldsForTemplateAsync(Guid templateId);
    
    Task DeleteFieldAsync(Guid id);
    Task<Field?> FindFieldByIdASync(Guid id);

    Task UpdateFieldAsync(Field field);

    Task AddQuestion(Guid fieldId);

    Task RemoveQuestion(Guid optionId);
}