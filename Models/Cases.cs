using System.Text.Json.Serialization;

namespace Testiny.Models
{
    public class Cases
    {
        [JsonPropertyName("data")] public Case[] CaseList { get; set; }
        [JsonPropertyName("meta")] public Meta Meta { get; set; }
    }
}