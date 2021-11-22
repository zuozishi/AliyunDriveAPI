namespace AliyunDriveAPI.Models;

public class FileUploadPartInfo
{
    public int PartNumber { get; set; }

    public FileUploadPartInfo() { }

    public FileUploadPartInfo(int partNumber)
    {
        PartNumber = partNumber;
    }
}

public class FileUploadPartInfoWithUrl : FileUploadPartInfo
{
    public string UploadUrl { get; set; }

    public string InternalUploadUrl { get; set; }

    public string ContentType { get; set; }

    public FileUploadPartInfoWithUrl() { }

    public FileUploadPartInfoWithUrl(int partNumber) : base(partNumber) { }
}