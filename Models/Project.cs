using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class Project
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string? ProjectName { get; set; }
        [JsonPropertyName("project_key")] public string? ProjectKey { get; set; }
        [JsonPropertyName("description")] public string? Description { get; set; }
        [JsonPropertyName("is_deleted")] public bool? IsDeleted { get; set; }
        [JsonPropertyName("created_at")] public string? CreatedAt { get; set; }
        [JsonPropertyName("created_by")] public int CreatedBy { get; set; }
        [JsonPropertyName("modified_at")] public string? ModifiedAt { get; set; }
        [JsonPropertyName("modified_by")] public int ModifiedBy { get; set; }
        [JsonPropertyName("deleted_at")] public string? DeletedAt { get; set; }
        [JsonPropertyName("deleted_by")] public int? DeletedBy { get; set; }
    }
}