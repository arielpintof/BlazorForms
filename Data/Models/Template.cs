using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorForms.Data.Models;

public class Template
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string AuthorId { get; set; }
    public ApplicationUser Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public IList<Field> Fields { get; set; } = new List<Field>();
    public IList<Comment> Comments { get; set; } = new List<Comment>();
}
