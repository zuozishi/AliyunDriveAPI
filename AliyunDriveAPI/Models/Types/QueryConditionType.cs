namespace AliyunDriveAPI.Models.Types;

public enum QueryConditionType
{
    [JsonPropertyName("match")]
    Match,
    [JsonPropertyName("=")]
    Equal,
    [JsonPropertyName(">")]
    GT,
    [JsonPropertyName(">=")]
    GE,
    [JsonPropertyName("<")]
    LT,
    [JsonPropertyName("<=")]
    LE
}