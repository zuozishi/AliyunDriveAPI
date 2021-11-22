namespace AliyunDriveAPI.Models.Request;

public class CreateFolderRequest
{
    public string DriveId { get; set; }

    public string ParentFileId { get; set; } = "root";

    public string Name { get; set; }

    public string CheckNameMode { get; set; } = "refuse";

    public FileType Type { get; set; } = FileType.Folder;
}