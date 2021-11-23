namespace AliyunDriveAPI.Models.Request;

public class CreateFolderRequest
{
    public string DriveId { get; set; }

    public string ParentFileId { get; set; } = "root";

    public string Name { get; set; }

    public CheckNameModeType CheckNameMode { get; set; } = CheckNameModeType.Refuse;

    public FileType Type { get; set; } = FileType.Folder;
}