using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models.Jira;

public class Fields
{
    [JsonPropertyName("project")]
    public Project Project { get; set; }
    [JsonPropertyName("summary")]
    public string Summary { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("issuetype")]
    public Issuetype Issuetype { get; set; }
    
    [JsonPropertyName("customfield_10055")]
    public string SubmitedBy { get; set; }
    
    [JsonPropertyName("customfield_10056")]
    public string? LinkInvocation { get; set; }
    
    [JsonPropertyName("customfield_10057")]
    public string? TemplateTitle { get; set; }

    [JsonPropertyName("priority")] 
    public Priority Priority { get; set; } = new();
}

public class Priority
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; }
}

public class Issuetype
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Project
{
    [JsonPropertyName("key")] 
    public string Key { get; set; }
}

public class Issue
{
    [JsonPropertyName("fields")] 
    public Fields Fields { get; set; } = new();
}