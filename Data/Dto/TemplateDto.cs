namespace BlazorForms.Data.Dto;

public class TemplateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorId { get; set; }
    public List<FieldDto> Fields { get; set; }
}