namespace AliyunDriveAPI.Models.Types;

public enum OrderByType
{
    [JsonPropertyName("name")]
    Name,
    [JsonPropertyName("size")]
    Size,
    [JsonPropertyName("updated_at")]
    UpdatedAt,
    [JsonPropertyName("created_at")]
    CreatedAt,
    [JsonPropertyName("custom_field_1")]
    CustomField_1,
    [JsonPropertyName("custom_field_2")]
    CustomField_2
}