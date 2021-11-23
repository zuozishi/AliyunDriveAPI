using System.Text.Json;

namespace AliyunDriveAPI.Utils;

internal static class JsonUtils
{
    public static T Clone<T>(object obj)
    {
        string json = JsonSerializer.Serialize(obj, AliyunDriveApiClient.JsonSerializerOptions);
        return JsonSerializer.Deserialize<T>(json);
    }
}