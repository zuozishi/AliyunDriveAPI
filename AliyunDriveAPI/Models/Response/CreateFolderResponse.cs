namespace AliyunDriveAPI.Models.Response;

public class CreateFolderResponse
{
    public string ParentFileId { get; set; }

    public FileType Type { get; set; }

    public string FileId { get; set; }

    public string DomainId { get; set; }

    public string DriveId { get; set; }

    public string FileName { get; set; }

    public string EncryptMode { get; set; }

    public string Status { get; set; }

    [JsonIgnore]
    public bool IsAvailable => Status == "available";

    public bool? Exist { get; set; }
}