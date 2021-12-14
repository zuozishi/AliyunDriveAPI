using System.Collections.Generic;

namespace AliyunDriveAPI.Models.Response;

public class FileShareResponse
{
    public DateTime CreatedAt { get; set; }

    public string Creator { get; set; }

    public string Description { get; set; }
    
    public int DownloadCount { get; set; }
    
    public string DriveId { get; set; }
    
    public DateTime? Expiration { get; set; }
    
    public bool Expired { get; set; }
    
    public string FileId { get; set; }
    
    public List<string> FileIdList { get; set; }
    
    public int PreviewCount { get; set; }
    
    public int SaveCount { get; set; }
    
    public string ShareId { get; set; }
    
    public string ShareMsg { get; set; }
    
    public string ShareName { get; set; }
    
    public string SharePolicy { get; set; }
    
    public string SharePwd { get; set; }
    
    public string ShareUrl { get; set; }
    
    public string Status { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
