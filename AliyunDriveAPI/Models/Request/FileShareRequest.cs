using System.Collections.Generic;

namespace AliyunDriveAPI.Models.Request;

public class FileShareRequest
{
    public string DriveId { get; set; }

    public List<string> FileIdList { get; set; }

    public DateTime? Expiration { get; set; }

    public bool SyncToHomepage { get; set; }

    public string SharePwd { get; set; }

    public FileShareRequest() { }

    public FileShareRequest(string driveId, string fileId, TimeSpan expiration)
        : this(driveId, fileId, expiration, null)
    {

    }

    public FileShareRequest(string driveId, string fileId, string sharePwd)
        : this(driveId, fileId, null, sharePwd)
    {

    }

    public FileShareRequest(string driveId, string fileId, TimeSpan? expiration = null, string sharePwd = null)
       : this(driveId, new List<string>() { fileId }, expiration, sharePwd)
    {
    }

    public FileShareRequest(string driveId, List<string> fileIdList, TimeSpan expiration)
        : this(driveId, fileIdList, expiration, null)
    {

    }

    public FileShareRequest(string driveId, List<string> fileIdList, string sharePwd)
        : this(driveId, fileIdList, null, sharePwd)
    {

    }

    public FileShareRequest(string driveId
                            , List<string> fileIdList
                            , TimeSpan? expiration = null
                            , string sharepwd = null
                            , bool syncToHomepage = false)
    {
        DriveId = driveId;
        FileIdList = fileIdList;
        if (expiration.HasValue)
        {
            Expiration = DateTime.Now.AddMilliseconds(expiration.Value.TotalMilliseconds);
        }
        SharePwd = sharepwd;
        SyncToHomepage = syncToHomepage;
    }
}