using BlazorForms.Data.Models;

namespace BlazorForms.Utils;

public static class FieldTypeUtils
{
    public static string ReadableName(this FieldType fieldType) => fieldType switch
    {
        FieldType.MultiSelection => "Multiple choice",
        FieldType.Checkbox => "Checkboxes",
        FieldType.PositiveInteger => "Positive Integer",
        FieldType.SingleLineString => "Short answer",
        FieldType.MultiLineText => "Paragraph",
        _ => "Unknown"
    };
}