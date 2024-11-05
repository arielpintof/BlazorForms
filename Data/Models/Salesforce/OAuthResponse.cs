using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace BlazorForms.Data.Models;

public class OAuthResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("instance_url")]
    public string InstanceUrl { get; set; }
    
    [JsonPropertyName("issued_at")]
    public string IssuedAt { get; set; }
    
    [JsonPropertyName("signature")]
    public string Signature { get; set; }
}