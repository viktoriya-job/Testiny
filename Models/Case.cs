using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class Case
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("is_deleted")] public bool IdDeleted { get; set; }
        [JsonPropertyName("project_id")] public int ProjectId { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("estimate")] public int Estimate { get; set; }
    }
}
