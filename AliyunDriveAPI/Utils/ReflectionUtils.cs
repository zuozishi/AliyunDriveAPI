using System.Linq;
using System.Reflection;

namespace AliyunDriveAPI.Utils;

public static class ReflectionUtils
{
    public static string GetEnumValueName(object value)
    {
        var enumType = value.GetType();
        var member = enumType.GetMember(value.ToString()).FirstOrDefault(m => m.DeclaringType == enumType);
        if (member != null)
        {
            var attr = member.GetCustomAttribute(typeof(JsonPropertyNameAttribute), false);
            if (attr != null)
                return (attr as JsonPropertyNameAttribute).Name;
        }
        return value.ToString();
    }
}