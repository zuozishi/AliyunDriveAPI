using System.Text.Json;

namespace AliyunDriveAPI.Models.Converters;

public class DatetimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (string.IsNullOrWhiteSpace(reader.GetString()))
            {
                return null;
            }
            if (DateTime.TryParse(reader.GetString(), out DateTime date))
                return date;
        }
        return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            // 2021-12-15T16:04:05.148Z
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
        }
        else
        {
            writer.WriteStringValue(string.Empty);
        }
    }
}