namespace BlazorForms.Data.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Template> Templates { get; set; } = new List<Template>();
}