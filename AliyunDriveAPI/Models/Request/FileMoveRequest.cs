namespace AliyunDriveAPI.Models.Request;

public class FileMoveRequest : FileBaseRequest
{
    public string ToDriveId { get; set; }

    public string ToParentFileId { get; set; }
}