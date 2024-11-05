using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models.Salesforce;

public class Accounts
{
    [JsonPropertyName("totalSize")]
    public int TotalSize { get; set; }
    [JsonPropertyName("done")]
    public bool Done { get; set; }
    [JsonPropertyName("records")]
    public List<Record> Records { get; set; }
}
