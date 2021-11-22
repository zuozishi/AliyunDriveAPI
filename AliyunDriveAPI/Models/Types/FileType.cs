namespace AliyunDriveAPI.Models.Types;

public enum FileType
{
    [JsonPropertyName("folder")]
    Folder,
    [JsonPropertyName("file")]
    File
}