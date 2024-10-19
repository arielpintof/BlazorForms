using Microsoft.Extensions.Localization;

namespace BlazorForms.Data.Enums;

public enum FieldType
{
    SingleLineString,
    MultiLineText,
    PositiveInteger,
    Checkbox,
    MultiSelection,
}

public static class FieldTypeExtensions
{
    public static string ReadableName(this FieldType fieldType, IStringLocalizer localizer) => fieldType switch
    {
        FieldType.MultiSelection => localizer["MultipleChoice"],
        FieldType.Checkbox => localizer["CheckBoxes"],
        FieldType.PositiveInteger => localizer["Number"],
        FieldType.SingleLineString => localizer["ShortAnswer"],
        FieldType.MultiLineText => localizer["Paragraph"],
        _ => "Unknown"
    };
    
}