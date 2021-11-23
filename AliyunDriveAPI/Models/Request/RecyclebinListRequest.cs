namespace AliyunDriveAPI.Models.Request;

public class RecyclebinListRequest : FileListRequest
{
    public RecyclebinListRequest()
    {
        ParentFileId = null;
        Limit = null;
        All = null;
        UrlExpireSec = null;
        Fields = null;
    }
}