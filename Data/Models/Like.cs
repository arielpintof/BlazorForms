using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models;

public class Like
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid TemplateId { get; set; }
    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore] public Template Template { get; set; } = null!;
    [JsonIgnore] public ApplicationUser User { get; set; } = null!;
}