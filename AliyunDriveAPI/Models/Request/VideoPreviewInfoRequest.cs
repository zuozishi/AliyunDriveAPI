namespace AliyunDriveAPI.Models.Request;

public class VideoPreviewInfoRequest : FileBaseRequest
{
    public string Category { get; set; } = "live_transcoding";

    public VideoPreviewTemplateType TemplateId { get; set; } = VideoPreviewTemplateType.NONE;
}