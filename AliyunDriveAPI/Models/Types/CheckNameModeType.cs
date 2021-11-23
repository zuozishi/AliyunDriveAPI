namespace AliyunDriveAPI.Models.Types;

public enum CheckNameModeType
{
    [JsonPropertyName("refuse")]
    Refuse,
    [JsonPropertyName("auto_rename")]
    AutoRename,
    [JsonPropertyName("overwrite")]
    Overwrite
}