namespace AliyunDriveAPI.Models.Response;

public class AudioPlayInfoResponse : ErrorResponse
{
    public AudioPlayInfo[] TemplateList { get; set; }
}