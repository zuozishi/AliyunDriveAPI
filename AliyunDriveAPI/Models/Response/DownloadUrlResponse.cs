namespace AliyunDriveAPI.Models.Response;

public class DownloadUrlResponse
{
    public string Method { get; set; }

    public string Url { get; set; }

    public string InternalUrl { get; set; }

    public DateTime Expiration { get; set; }

    public long Size { get; set; }

    public RateLimit RateLimit { get; set; }

    public string Crc64Hash { get; set; }

    public string ContentHash { get; set; }

    public string ContentHashName { get; set; }
}