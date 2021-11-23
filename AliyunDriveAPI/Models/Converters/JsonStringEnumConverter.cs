using System.Text.Json;

namespace AliyunDriveAPI.Models.Converters;

public class JsonStringEnumConverter : JsonConverterFactory
{
    private readonly JsonNamingPolicy _namingPolicy;
    private readonly EnumConverterOptions _converterOptions;

    /// <summary>
    /// Constructor. Creates the <see cref="JsonStringEnumConverter"/> with the
    /// default naming policy and allows integer values.
    /// </summary>
    public JsonStringEnumConverter()
        : this(namingPolicy: null, allowIntegerValues: true)
    {
        // An empty constructor is needed for construction via attributes
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="namingPolicy">
    /// Optional naming policy for writing enum values.
    /// </param>
    /// <param name="allowIntegerValues">
    /// True to allow undefined enum values. When true, if an enum value isn't
    /// defined it will output as a number rather than a string.
    /// </param>
    public JsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
    {
        _namingPolicy = namingPolicy;
        _converterOptions = allowIntegerValues
            ? EnumConverterOptions.AllowNumbers | EnumConverterOptions.AllowStrings
            : EnumConverterOptions.AllowStrings;
    }

    /// <inheritdoc />
    public sealed override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    /// <inheritdoc />
    public sealed override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        EnumConverterFactory.Create(typeToConvert, _converterOptions, _namingPolicy, options);
}

[Flags]
internal enum EnumConverterOptions
{
    /// <summary>
    /// Allow string values.
    /// </summary>
    AllowStrings = 0b0001,

    /// <summary>
    /// Allow number values.
    /// </summary>
    AllowNumbers = 0b0010
}