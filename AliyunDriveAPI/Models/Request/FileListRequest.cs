namespace AliyunDriveAPI.Models.Request;

public class FileListRequest
{
    public string DriveId { get; set; }

    public string ParentFileId { get; set; } = "root";

    public int? Limit { get; set; } = 100;

    public bool? All { get; set; } = false;

    public int? UrlExpireSec { get; set; } = 1600;

    public string ImageThumbnailProcess { get; set; } = "image/resize,w_400/format,jpeg";

    public string ImageUrlProcess { get; set; } = "image/resize,w_1920/format,jpeg";

    public string VideoThumbnailProcess { get; set; } = "video/snapshot,t_0,f_jpg,ar_auto,w_300";

    public string Fields { get; set; } = "*";

    public OrderByType? OrderBy { get; set; } = OrderByType.UpdatedAt;

    public OrderDirectionType? OrderDirection { get; set; } = OrderDirectionType.DESC;
}