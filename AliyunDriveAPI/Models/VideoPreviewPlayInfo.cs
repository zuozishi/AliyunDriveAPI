namespace AliyunDriveAPI.Models;

public class VideoPreviewPlayInfo
{
    public string Category { get; set; }
    public VideoPreviewPlayInfoMeta Meta { get; set; }
    public LiveTranscodingTask[] LiveTranscodingTaskList { get; set; }
}

public class VideoPreviewPlayInfoMeta
{
    public TimeSpan Duration { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public LiveTranscodingMeta LiveTranscodingMeta { get; set; }
}

public class LiveTranscodingMeta
{
    public int TsSegment { get; set; }
    public int TsTotalCount { get; set; }
    public int TsPreCount { get; set; }
}

public class LiveTranscodingTask
{
    public VideoPreviewTemplateType TemplateId { get; set; }
    public string TemplateName { get; set; }
    public string Status { get; set; }
    public string Stage { get; set; }
    public string Url { get; set; }
    [JsonIgnore]
    public bool IsFinished => Status == "finished";
}