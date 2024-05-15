using System.Text.Json.Serialization;

namespace PostsApi.Core.Domain;

public class Post : BaseEntity
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("friendlyUrl")]
    public string FriendlyUrl { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; }
}
