namespace AliyunDriveAPI.Models.Response;

public class OfficePreviewUrlResponse : ErrorResponse
{
    public string PreviewUrl { get; set; }

    public string AccessToken { get; set; }
}