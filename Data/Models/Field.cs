using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models;

public class Field
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool ShowInTable { get; set; }
    public int Order { get; set; }
    public Guid TemplateId { get; set; }
   
    public FieldType Type { get; set; }
    public IList<Option> Options { get; set; } = new List<Option>();
    
    [JsonIgnore]
    public Template Template { get; set; } = null!;
}

public enum FieldType
{
    SingleLineString,
    MultiLineText,
    PositiveInteger,
    Checkbox,
    MultiSelection,
}
