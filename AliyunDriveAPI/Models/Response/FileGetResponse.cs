namespace AliyunDriveAPI.Models.Response;

public class FileGetResponse : FileItem
{
    public ExFieldsInfo ExFieldsInfo { get; set; }

    public bool Trashed { get; set; }
}