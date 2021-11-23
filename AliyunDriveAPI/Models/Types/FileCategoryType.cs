namespace AliyunDriveAPI.Models.Types;

public enum FileCategoryType
{
    [JsonPropertyName("image")]
    Image,
    [JsonPropertyName("video")]
    Video,
    [JsonPropertyName("audio")]
    Audio,
    [JsonPropertyName("app")]
    APP,
    [JsonPropertyName("doc")]
    Doc,
    [JsonPropertyName("zip")]
    Zip,
    [JsonPropertyName("others")]
    Others
}