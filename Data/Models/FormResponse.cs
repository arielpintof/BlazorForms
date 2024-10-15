using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models;

public class FormResponse
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public string ResponderId { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public IList<FieldResponse> FieldResponses { get; set; } = new List<FieldResponse>();

    [JsonIgnore] public Template Template { get; set; } = null!;
    [JsonIgnore] public ApplicationUser Responder { get; set; } = null!;
}