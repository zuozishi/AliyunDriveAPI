namespace AliyunDriveAPI.Models.Request;

public class PreUploadRequest
{
    public string DriveId { get; set; }

    public string ParentFileId { get; set; } = "root";

    public FileUploadPartInfo[] PartInfoList { get; set; }

    public string Name { get; set; }

    public FileType Type { get; set; } = FileType.File;

    public CheckNameModeType CheckNameMode { get; set; }

    public string ContentHash { get; set; }

    public string ContentHashName { get; set; }

    public string ProofCode { get; set; }

    public string ProffVersion { get; set; }
}