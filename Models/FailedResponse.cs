using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class FailedResponse
    {
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("code")] public string Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("reqid")] public string Reqid { get; set; }
    }
}