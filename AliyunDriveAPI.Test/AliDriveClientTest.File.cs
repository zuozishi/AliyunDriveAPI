using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using System;
using AliyunDriveAPI.Models;

namespace AliyunDriveAPI.Test;

public partial class AliDriveClientTest
{
    private string _fileId;

    [Test(Description = "创建文件夹"), Category("Folder"), Order(1)]
    public async Task CreateFolderAsync()
    {
        var res = await _client.CreateFolderAsync(DriveId, "CreateFolderTest", ParentFileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }

    [Test(Description = "上传文件"), Category("File"), Order(2)]
    public async Task UploadTextFileAsync()
    {
        var res = await _client.UploadFileAsync(DriveId, "test.txt", Encoding.UTF8.GetBytes(DateTime.Now.ToString("😝yyyy/MM/dd HH:mm:ss😂")), ParentFileId, Models.Types.CheckNameModeType.Overwrite);
        _fileId = res?.FileId;
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }

    [Test(Description = "获取文件下载链接"), Category("File"), Order(3)]
    public async Task GetDownloadUrlAsync()
    {
        var res = await _client.GetDownloadUrlAsync(DriveId, _fileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.Url));
    }

    [Test(Description = "搜索文件"), Category("File"), Order(4)]
    public async Task SampleSearchFileAsync()
    {
        var sampleQuery = new SampleFileSearchQuery
        {
            Name = "test.flac",
            ParentFileId = ParentFileId,
            Category = Models.Types.FileCategoryType.Audio,
            Type = Models.Types.FileType.File
        };
        var res = await _client.FileSearchAsync(DriveId, sampleQuery);
        Assert.IsTrue(res.Items.Length > 0);
    }

    [Test(Description = "删除文件"), Category("File"), Order(5)]
    public async Task DeleteFileAsync()
    {
        await _client.DeleteFileAsync(DriveId, _fileId);
        Assert.Pass();
    }
}