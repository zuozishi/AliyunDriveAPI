namespace AliyunDriveAPI.Models.Response;

public class VideoPreviewInfoResponse
{
    public string DomainId { get; set; }

    public string DriveId { get; set; }

    public string FileId { get; set; }

    public VideoPreviewPlayInfo VideoPreviewPlayInfo { get; set; }
}