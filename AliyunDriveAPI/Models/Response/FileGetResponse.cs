namespace AliyunDriveAPI.Models.Response;

public class FileGetResponse : FileItem
{
    public ExFieldsInfo ExFieldsInfo { get; set; }

    public bool Trashed { get; set; }

    public string Code { get; set; }
    public string Message { get; set; }
    public string RequestId { get; set; }
    public bool IsOk => string.IsNullOrEmpty(Code);
}