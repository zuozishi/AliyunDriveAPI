namespace AliyunDriveAPI.Models.Types;

public enum QueryRelationType
{
    [JsonPropertyName("and")]
    AND,
    [JsonPropertyName("or")]
    OR
}