namespace AliyunDriveAPI.Models.Response;

public class ErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public string RequestId { get; set; }
    public bool IsOk => string.IsNullOrEmpty(Code);
}