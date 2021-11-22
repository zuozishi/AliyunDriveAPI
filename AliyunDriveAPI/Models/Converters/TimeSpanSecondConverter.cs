using System.Text.Json;

namespace AliyunDriveAPI.Models.Converters;

public class TimeSpanSecondConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string text = reader.GetString();
            if(double.TryParse(text, out var sec))
                return TimeSpan.FromSeconds(sec);
        }else if(reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetDouble(out var sec))
                return TimeSpan.FromSeconds(sec);
        }
        return default;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.TotalSeconds);
}

public class NullableTimeSpanSecondConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string text = reader.GetString();
            if (double.TryParse(text, out var sec))
                return TimeSpan.FromSeconds(sec);
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetDouble(out var sec))
                return TimeSpan.FromSeconds(sec);
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value.TotalSeconds);
        else
            writer.WriteNullValue();
    }
}