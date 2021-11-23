using AliyunDriveAPI.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AliyunDriveAPI.Test;

public partial class AliDriveClientTest
{
    private AliyunDriveApiClient _client;
    private const string DriveId = "38023";
    private const string ParentFileId = "619bf787e124392830214dafb30c4d6ee97dd9a2";

    [SetUp]
    public void Setup()
    {
        string refreshToken = File.Exists("refresh_token") ?
            File.ReadAllText("refresh_token") :
            Environment.GetEnvironmentVariable("REFRESH_TOKEN");
        _client = new AliyunDriveApiClient(refreshToken);
    }

    [Test(Description = "列出文件")]
    public async Task FileListAsync()
    {
        var res = await _client.FileListAsync(new()
        {
            DriveId = DriveId
        });
        Assert.IsTrue(res.Items.Length > 0);
    }

    [Test]
    public async Task FileGetAsync()
    {
        var res = await _client.FileGetAsync(DriveId, ParentFileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }
}