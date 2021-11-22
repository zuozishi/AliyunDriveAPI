namespace AliyunDriveAPI.Models;

public class AudioPlayInfo
{
    public AudioPlayInfoTemplateType TemplateId { get; set; }

    public string Status { get; set; }

    [JsonIgnore]
    public bool IsFinished => Status == "status";

    public string Url { get; set; }
}