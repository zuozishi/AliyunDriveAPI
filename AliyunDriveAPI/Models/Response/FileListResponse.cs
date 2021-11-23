namespace AliyunDriveAPI.Models.Response;

public class FileListResponse
{
    public FileItem[] Items { get; set; }

    public string NextMarker { get; set; }

    public int PunishedFileCount { get; set; }
}