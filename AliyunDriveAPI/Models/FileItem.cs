using System.Text.Json.Nodes;

namespace AliyunDriveAPI.Models;

public class FileItem
{
    #region BaseInfos
    public string DriveId { get; set; }

    public string DomainId { get; set; }

    public string FileId { get; set; }

    public string Name { get; set; }

    [JsonConverter(typeof(Converters.JsonStringEnumConverter))]
    public FileType Type { get; set; }

    [JsonIgnore]
    public bool IsFile => Type == FileType.File;

    [JsonIgnore]
    public bool IsFolder => Type == FileType.Folder;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Hidden { get; set; }

    public bool Starred { get; set; }

    public string Status { get; set; }

    [JsonIgnore]
    public bool IsAvailable => Status == "available";

    public string ParentFileId { get; set; }

    public string EncryptMode { get; set; }

    public string RevisionId { get; set; }

    public JsonNode UserMeta { get; set; }
    #endregion

    #region EditorAndModifier
    public string CreatorType { get; set; }

    public string CreatorId { get; set; }

    public string CreatorName { get; set; }

    public string LastModifierType { get; set; }

    public string LastModifierId { get; set; }

    public string LastModifierName { get; set; }
    #endregion

    #region FileInfos
    public string ContentType { get; set; }

    public string MimeType { get; set; }

    public string MimeExtension { get; set; }

    public long? Size { get; set; }

    public string UploadId { get; set; }

    public string Crc64Hash { get; set; }

    public string ContentHash { get; set; }

    public string ContentHashName { get; set; }

    public string DownloadUrl { get; set; }

    public string Url { get; set; }

    public string Category { get; set; }

    [JsonIgnore]
    public FileCategoryType? CategoryType
        => Category switch
        {
            null => null,
            "image" => FileCategoryType.Image,
            "video" => FileCategoryType.Video,
            "audio" => FileCategoryType.Audio,
            "app" => FileCategoryType.APP,
            "doc" => FileCategoryType.Doc,
            "zip" => FileCategoryType.Zip,
            _ => FileCategoryType.Others
        };

    public int? PunishFlag { get; set; }
    #endregion

    #region MediaInfos
    public string[] Labels { get; set; }

    public string Thumbnail { get; set; }

    public ImageMediaMetadata ImageMediaMetadata { get; set; }

    public VideoMediaMetadata VideoMediaMetadata { get; set; }
    
    public VideoPreviewMetadata VideoPreviewMetadata { get; set; }

    [JsonIgnore]
    public AudioMediaMetadata AudioMediaMetadata => VideoPreviewMetadata ?? null;
    #endregion
}

#region ImageMetadata
public class ImageMediaMetadata
{
    public int Width { get; set; }
    public int Height { get; set; }
    public ImageTag[] ImageTags { get; set; }
    public JsonNode Exif { get; set; }
    public ImageQuality ImageQuality { get; set; }
    public CroppingSuggestion[] CroppingSuggestion { get; set; }
}

public class ImageTag
{
    public decimal Confidence { get; set; }
    public string ParentName { get; set; }
    public string Name { get; set; }
    public int TagLevel { get; set; }
    public decimal CentricScore { get; set; }
}

public class ImageQuality
{
    public decimal OverallScore { get; set; }
}

public class CroppingSuggestion
{
    public string AspectRatio { get; set; }
    public decimal Score { get; set; }
    public CroppingBoundary CroppingBoundary { get; set; }
}

public class CroppingBoundary
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
}
#endregion

#region VideoMetadata
public class VideoPreviewMetadata : AudioMediaMetadata
{
    public string VideoFormat { get; set; }
    public string FrameRate { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}

public class VideoMediaMetadata
{
    public int Width { get; set; }
    public int Height { get; set; }
    public ImageTag[] ImageTags { get; set; }
    public VideoMediaVideoStream[] VideoMediaVideoStream { get; set; }
    public VideoMediaAudioStream[] VideoMediaAudioStream { get; set; }
    public TimeSpan Duration { get; set; }
}

public class VideoMediaVideoStream
{
    public TimeSpan Duration { get; set; }
    public string Clarity { get; set; }
    public string Fps { get; set; }
    public string Bitrate { get; set; }
    public string CodeName { get; set; }
}

public class VideoMediaAudioStream
{
    public TimeSpan Duration { get; set; }
    public int Channels { get; set; }
    public string ChannelLayout { get; set; }
    public string BitRate { get; set; }
    public string CodeName { get; set; }
    public string SampleRate { get; set; }
}
#endregion

#region AudioMetadata
public class AudioMediaMetadata
{
    public string Bitrate { get; set; }
    public string Duration { get; set; }
    public string AudioFormat { get; set; }
    public string AudioSampleRate { get; set; }
    public int AudioChannels { get; set; }
    public AudioTemplate[] AudioTemplateList { get; set; }
    public AudioMeta AudioMeta { get; set; }
    public AudioMusicMeta AudioMusicMeta { get; set; }
}

public class AudioTemplate
{
    public string TemplateId { get; set; }
    public string Status { get; set; }
    [JsonIgnore]
    public bool IsFinished => Status == "finished";
}

public class AudioMeta
{
    public long Bitrate { get; set; }
    public TimeSpan Duration { get; set; }
    public long SampleRate { get; set; }
    public int Channels { get; set; }
}

public class AudioMusicMeta
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string CoverUrl { get; set; }
}
#endregion

public class ExFieldsInfo
{
    public string VideoMetaProcessed { get; set; }

    [JsonIgnore]
    public bool IsVideoMetaProcessed => VideoMetaProcessed == "y";
}