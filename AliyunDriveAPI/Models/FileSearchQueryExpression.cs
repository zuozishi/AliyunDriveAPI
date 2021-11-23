using AliyunDriveAPI.Utils;
using System.Collections.Generic;
using System.Text;

namespace AliyunDriveAPI.Models;

public class FileSearchQueryExpression : List<FileSearchQuery>
{
    public QueryRelationType RelationType { get; set; } = QueryRelationType.AND;

    public FileSearchQueryExpression Children { get; set; }

    public FileSearchQueryExpression() { }

    public FileSearchQueryExpression(IEnumerable<FileSearchQuery> conditions) : base(conditions) { }

    public FileSearchQueryExpression(IEnumerable<FileSearchQuery> conditions, QueryRelationType relationType) : base(conditions)
    {
        RelationType = relationType;
    }

    public FileSearchQueryExpression(QueryRelationType relationType, FileSearchQueryExpression children)
    {
        RelationType = relationType;
        Children = children;
    }

    public override string ToString()
    {
        string childrenText = string.Empty;
        if (Children != null)
            childrenText = Children.ToString();
        var sb = new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            var text = this[i].ToString();
            sb.Append(text);
            if(i != Count - 1)
                sb.Append(" " + ReflectionUtils.GetEnumValueName(this[i].NearRelationType) + " ");
        }
        string thisText = sb.ToString();
        sb = sb.Clear();
        if (!string.IsNullOrEmpty(childrenText))
        {
            if (!string.IsNullOrEmpty(thisText))
            {
                sb.Append("(" + thisText + ") ");
                sb.Append(ReflectionUtils.GetEnumValueName(RelationType) + " ");
                sb.Append("(" + childrenText + ")");
            }
            else
                sb.Append(childrenText);
        }
        else
            sb.Append(thisText);
        return sb.ToString();
    }
}