namespace AliyunDriveAPI.Models.Response;

public class PreUploadResponse
{
    public string ParentFileId { get; set; }

    public FileUploadPartInfoWithUrl[] PartInfoList { get; set; }

    public string UploadId { get; set; }

    public bool RepidUpload { get; set; }

    public FileType Type { get; set; }

    public string FileId { get; set; }

    public string DomainId { get; set; }

    public string DriveId { get; set; }

    public string FileName { get; set; }

    public string EncryptMode { get; set; }

    public string Location { get; set; }
}