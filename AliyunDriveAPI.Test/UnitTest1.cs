using AliyunDriveAPI.Models.Request;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AliyunDriveAPI.Test;

public class Tests
{
    private AliyunDriveApiClient _client;

    [SetUp]
    public void Setup()
    {
        string refreshToken = File.Exists("refresh_token") ?
            File.ReadAllText("refresh_token") :
            Environment.GetEnvironmentVariable("REFRESH_TOKEN");
        _client = new AliyunDriveApiClient(refreshToken);
    }

    [Test]
    public async Task RefreshTokenAsync()
    {
        var res = await _client.RefreshTokenAsync();
        Assert.IsTrue(!string.IsNullOrWhiteSpace(res.AccessToken));
    }

    [Test]
    public async Task FileListAsync()
    {
        var req = FileListRequest.Default();
        req.DriveId = "38023";
        var res = await _client.FileListAsync(req);
        Assert.IsTrue(res.Items.Length > 0);
    }

    [Test]
    public async Task GetVideoPreviewInfoAsync()
    {
        var req = VideoPreviewInfoRequest.Default();
        req.DriveId = "38023";
        req.FileId = "614ffe5c030b1a077c1e4281bfe952ed2df27472";
        var res = await _client.GetVideoPreviewPlayInfo(req);
        Assert.IsTrue(res.VideoPreviewPlayInfo.LiveTranscodingTaskList.Length > 0);
    }
}