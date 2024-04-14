using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class Case
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
    }
}
