using System.Text.Json.Serialization;
using static BaseSolution.Application.ValueObjects.Common.QueryConstant;

namespace BaseSolution.Application.ValueObjects.Common;

public class SearchModel
{
    [JsonPropertyName("search_field_name")]
    public string SearchFieldName { get; set; } = string.Empty;
    [JsonPropertyName("search_value")]
    public string? SearchValue { get; set; } = string.Empty;
    [JsonPropertyName("match_type")]
    public bool MatchType { get; set; } = MatchTypes.Contain;
}