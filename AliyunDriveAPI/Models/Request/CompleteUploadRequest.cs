namespace AliyunDriveAPI.Models.Request;

public class CompleteUploadRequest : FileBaseRequest
{
    public string UploadId { get; set; }
}