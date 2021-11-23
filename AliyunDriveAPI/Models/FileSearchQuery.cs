using AliyunDriveAPI.Utils;

namespace AliyunDriveAPI.Models;

public class FileSearchQuery
{
    public string Name { get; set; }

    public object Value { get; set; }

    public QueryConditionType ConditionType { get; set; }

    public QueryRelationType NearRelationType { get; set; } = QueryRelationType.AND;

    public FileSearchQuery() { }

    public FileSearchQuery(string name, object value, QueryConditionType type)
    {
        Name = name;
        Value = value;
        ConditionType = type;
    }

    public FileSearchQuery(string name, object value, QueryConditionType type, QueryRelationType relationType)
    {
        Name = name;
        Value = value;
        ConditionType = type;
        NearRelationType = relationType;
    }

    public override string ToString()
    {
        bool isString = Value is string;
        return isString ? $"{Name} {ReflectionUtils.GetEnumValueName(ConditionType)} \"{Value}\""
            : $"{Name} {ReflectionUtils.GetEnumValueName(ConditionType)} {Value}";
    }
}