using System;
using System.Collections.Generic;
using System.Text;

namespace AliyunDriveAPI.Models.Request
{
    public class FileShareRequest
    {
        public string DriveId { get; set; }

        public List<string> FileIdList { get; set; }

        public DateTime Expiration { get; set; }

        public bool SyncToHomepage { get; set; }

        public FileShareRequest() { }

        public FileShareRequest(string driveId, string fileId, TimeSpan expiration) : this(driveId, new List<string>() { fileId }, expiration)
        {
        }

        public FileShareRequest(string driveId, List<string> fileIdList, TimeSpan expiration)
        {
            DriveId = driveId;
            FileIdList = fileIdList;
            Expiration = DateTime.Now.AddMilliseconds(expiration.TotalMilliseconds);
        }
    }
}
