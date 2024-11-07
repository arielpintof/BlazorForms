using Newtonsoft.Json;

namespace BlazorForms.Data.Models.Jira;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ApplicationRoles
{
    public int size { get; set; }
    public List<object> items { get; set; }
}

public class AvatarUrls
{
    [JsonProperty("48x48")]
    public string _48x48 { get; set; }

    [JsonProperty("24x24")]
    public string _24x24 { get; set; }

    [JsonProperty("16x16")]
    public string _16x16 { get; set; }

    [JsonProperty("32x32")]
    public string _32x32 { get; set; }
}

public class Groups
{
    public int size { get; set; }
    public List<object> items { get; set; }
}

public class UserCreatedResponse
{
    public string self { get; set; }
    public string accountId { get; set; }
    public string accountType { get; set; }
    public string emailAddress { get; set; }
    public AvatarUrls avatarUrls { get; set; }
    public string displayName { get; set; }
    public bool active { get; set; }
    public string timeZone { get; set; }
    public string locale { get; set; }
    public Groups groups { get; set; }
    public ApplicationRoles applicationRoles { get; set; }
    public string expand { get; set; }
}

