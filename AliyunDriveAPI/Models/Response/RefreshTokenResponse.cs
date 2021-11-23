namespace AliyunDriveAPI.Models.Response;

public class RefreshTokenResponse
{
    public string DefaultSboxDriveId { get; set; }

    public string Role { get; set; }

    public string DeviceId { get; set; }

    public string UserName { get; set; }

    public bool NeedLink { get; set; }

    public DateTime? ExpireTime { get; set; }

    public bool? PinSetup { get; set; }

    public bool? NeedRpVerify { get; set; }

    public string Avatar { get; set; }

    public string TokenType { get; set; }

    public string AccessToken { get; set; }

    public string DefaultDriveId { get; set; }

    public string DomainId { get; set; }

    public string RefreshToken { get; set; }

    public bool? IsFirstLogin { get; set; }

    public string UserId { get; set; }

    public string NickName { get; set; }

    public string State { get; set; }

    public int? ExpiresIn { get; set; }

    public string Status { get; set; }
}