using BlazorForms.Data.Enums;
using BlazorForms.Data.Models;

namespace BlazorForms.Data.Dto;

public class FieldDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool ShowInTable { get; set; }
    public FieldType Type { get; set; }
    public int Order { get; set; }
}