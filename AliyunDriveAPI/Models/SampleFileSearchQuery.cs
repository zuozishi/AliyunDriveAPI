using AliyunDriveAPI.Utils;

namespace AliyunDriveAPI.Models;

public class SampleFileSearchQuery
{
    public string Name { get; set; }

    public string ParentFileId { get; set; }

    public FileCategoryType? Category { get; set; }

    public FileType? Type { get; set; }

    public FileSearchQueryExpression GetQueryExpression()
    {
        var queryExpression = new FileSearchQueryExpression();
        if (Name != null)
            queryExpression.Add(new("name", Name, QueryConditionType.Match));
        if (ParentFileId != null)
            queryExpression.Add(new("parent_file_id", ParentFileId, QueryConditionType.Equal));
        if (Category != null)
            queryExpression.Add(new("category", ReflectionUtils.GetEnumValueName(Category.Value), QueryConditionType.Equal));
        if (Type != null)
            queryExpression.Add(new("type", ReflectionUtils.GetEnumValueName(Type.Value), QueryConditionType.Equal));
        return queryExpression;
    }

    public override string ToString()
        => GetQueryExpression().ToString();
}