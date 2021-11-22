using System.Text.Json;

namespace AliyunDriveAPI.Models.Request;

public class FileListRequest
{
    public static FileListRequest Default()
        => new()
        {
            ParentFileId = "root",
            Limit = 100,
            All = false,
            UrlExpireSec = 1600,
            ImageThumbnailProcess = "image/resize,w_400/format,jpeg",
            ImageUrlProcess = "image/resize,w_1920/format,jpeg",
            VideoThumbnailProcess = "video/snapshot,t_0,f_jpg,ar_auto,w_300",
            Fields = "*",
            OrderBy = OrderByType.UpdatedAt,
            OrderDirection = OrderDirectionType.DESC
        };

    public string DriveId { get; set; }

    public string ParentFileId { get; set; }

    public int? Limit { get; set; }

    public bool? All { get; set; }

    public int? UrlExpireSec { get; set; }

    public string ImageThumbnailProcess { get; set; }

    public string ImageUrlProcess { get; set; }

    public string VideoThumbnailProcess { get; set; }

    public string Fields { get; set; }

    public OrderByType? OrderBy { get; set; }

    public OrderDirectionType? OrderDirection { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this, AliyunDriveApiClient.JsonSerializerOptions);
}