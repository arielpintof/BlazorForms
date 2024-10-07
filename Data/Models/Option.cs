using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models;

public class Option
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public Guid FieldId { get; set; }
    
    [JsonIgnore]
    public Field Field { get; set; } = null!;
    
}

public static class OptionExtension
{
    public static int? ToIntValue(this Option option)
    {
        if (int.TryParse(option.Value, out var result))
        {
            return result;
        }
        return null;
    }
}