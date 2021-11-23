using System.Text.Json;

namespace AliyunDriveAPI.Models.Converters;

internal sealed class EnumConverterFactory : JsonConverterFactory
{
    public EnumConverterFactory()
    {
    }

    public override bool CanConvert(Type type)
    {
        return type.IsEnum;
    }

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options) =>
        Create(type, EnumConverterOptions.AllowNumbers, options);

    internal static JsonConverter Create(Type enumType, EnumConverterOptions converterOptions, JsonSerializerOptions serializerOptions)
    {
        return (JsonConverter)Activator.CreateInstance(
            GetEnumConverterType(enumType),
            new object[] { converterOptions, serializerOptions })!;
    }

    internal static JsonConverter Create(Type enumType, EnumConverterOptions converterOptions, JsonNamingPolicy namingPolicy, JsonSerializerOptions serializerOptions)
    {
        return (JsonConverter)Activator.CreateInstance(
            GetEnumConverterType(enumType),
            new object[] { converterOptions, namingPolicy, serializerOptions })!;
    }

    private static Type GetEnumConverterType(Type enumType) => typeof(EnumConverter<>).MakeGenericType(enumType);
}