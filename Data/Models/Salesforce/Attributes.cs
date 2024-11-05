using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models.Salesforce;

public class Attributes
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
}