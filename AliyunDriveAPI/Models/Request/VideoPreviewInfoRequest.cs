namespace AliyunDriveAPI.Models.Request;

public class VideoPreviewInfoRequest
{
    public static VideoPreviewInfoRequest Default()
        => new()
        {
            Category = "live_transcoding",
            TemplateId = VideoPreviewTemplateType.NONE
        };

    public string DriveId { get; set; }

    public string FileId { get; set; }

    public string Category { get; set; }

    public VideoPreviewTemplateType TemplateId { get; set; }
}