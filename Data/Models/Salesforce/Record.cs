using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models.Salesforce;

public class Record
{
    [JsonPropertyName("attributes")]
    public Attributes Attributes { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    public string? ShippingCountry { get; set; }
}