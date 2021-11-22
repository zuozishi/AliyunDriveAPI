using System.Text.Json;
using System.Text.Json.Nodes;

namespace AliyunDriveAPI.Models.Converters;

public class JsonNodeConverter : JsonConverter<JsonNode>
{
    public override JsonNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string json = reader.GetString();
        return json != null ? JsonNode.Parse(json) : null;
    }

    public override void Write(Utf8JsonWriter writer, JsonNode value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.ToString());
    }
}