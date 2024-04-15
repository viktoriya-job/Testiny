using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class Meta
    {
        [JsonPropertyName("count")] public int Count { get; set; }
        [JsonPropertyName("limit")] public int Limit { get; set; }
        [JsonPropertyName("offset")] public int Offset { get; set; }
        [JsonPropertyName("totalCount")] public int totalCount { get; set; }

        public override string? ToString()
        {
            return $"{ nameof(Count)}: {Count}, " +
                $"{nameof(Limit)}: {Limit}, " +
                $"{nameof(Offset)}: {Offset}, " +
                $"{nameof(totalCount)}: {totalCount}";
        }
    }
}