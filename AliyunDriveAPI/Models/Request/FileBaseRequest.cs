namespace AliyunDriveAPI.Models.Request;

public class FileBaseRequest
{
    public string DriveId { get; set; }

    public string FileId { get; set; }

    public FileBaseRequest() { }

    public FileBaseRequest(string driveId, string fileId)
    {
        DriveId = driveId;
        FileId = fileId;
    }
}