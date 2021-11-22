namespace AliyunDriveAPI.Models.Request;

public class FileGetRequest : FileBaseRequest
{
    public int? UrlExpireSec { get; set; } = 1600;

    public string ImageThumbnailProcess { get; set; } = "image/resize,w_400/format,jpeg";

    public string ImageUrlProcess { get; set; } = "image/resize,w_1920/format,jpeg";

    public string VideoThumbnailProcess { get; set; } = "video/snapshot,t_0,f_jpg,ar_auto,w_300";
}