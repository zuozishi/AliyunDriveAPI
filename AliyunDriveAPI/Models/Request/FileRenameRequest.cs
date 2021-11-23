namespace AliyunDriveAPI.Models.Request;

public class FileRenameRequest : FileBaseRequest
{
    public string Name { get; set; }

    public CheckNameModeType CheckNameMode { get; set; } = CheckNameModeType.Refuse;
}