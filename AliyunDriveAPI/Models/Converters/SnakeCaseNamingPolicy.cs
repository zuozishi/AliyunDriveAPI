using System.Linq;
using System.Text.Json;

namespace AliyunDriveAPI.Models.Converters;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLowerInvariant(x).ToString() : char.ToLowerInvariant(x).ToString()));
}