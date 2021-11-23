using AliyunDriveAPI.Utils;

namespace AliyunDriveAPI.Models.Request;

public class FileSearchRequest
{
    public string DriveId { get; set; }

    public int? Limit { get; set; } = 100;

    public string Marker { get; set; }

    public string Query { get; set; }

    public string ImageThumbnailProcess { get; set; } = "image/resize,w_400/format,jpeg";

    public string ImageUrlProcess { get; set; } = "image/resize,w_1920/format,jpeg";

    public string VideoThumbnailProcess { get; set; } = "video/snapshot,t_0,f_jpg,ar_auto,w_300";

    public OrderByType? OrderByType { get; set; } = Types.OrderByType.UpdatedAt;

    public OrderDirectionType? OrderDirection { get; set; } = OrderDirectionType.DESC;

    public string OrderBy => OrderByType.HasValue && OrderDirection.HasValue
        ? ReflectionUtils.GetEnumValueName(OrderByType.Value) + " " + ReflectionUtils.GetEnumValueName(OrderDirection.Value) : null;
}