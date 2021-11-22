using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliyunDriveAPI.Test;

public class Tests
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

    [Test]
    public async Task RefreshTokenAsync()
    {
        var res = await _client.RefreshTokenAsync();
        Assert.IsTrue(!string.IsNullOrWhiteSpace(res.AccessToken));
    }

    [Test]
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
        var res = await _client.FileGetAsync(DriveId, "619b0e54df844ed38e9140a4874e85b9c732b721");
        Assert.IsTrue(!string.IsNullOrEmpty(res.Url));
    }

    [Test]
    public async Task CreateFolderAsync()
    {
        var res = await _client.CreateFolderAsync(DriveId, "音乐", "root");
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }

    [Test]
    public async Task UploadTextFileAsync()
    {
        var res = await _client.UploadFileAsync(DriveId, "test.txt", Encoding.UTF8.GetBytes(DateTime.Now.ToString("😝yyyy/MM/dd HH:mm:ss😂")), ParentFileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }

    /*
    [Test]
    public async Task UploadLargeFileAsync()
    {
        using var stream = File.OpenRead(@"D:\Media\混合内容\杜比全景声Amaze.mp4");
        var res = await _client.UploadFileAsync(DriveId, "杜比全景声Amaze.mp4", stream, ParentFileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }

    [Test]
    public async Task UploadMusicFileAsync()
    {
        string url = "https://webfs.tx.kugou.com/202111230332/7b0fc2e18f5542a6904bb4068b38e882/KGTX/CLTX001/75b44dee9f8e41f4118d515ff4268f7f.flac";
        using var hc = new HttpClient();
        using var resp = await hc.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        using var stream = await resp.Content.ReadAsStreamAsync();
        var res = await _client.UploadFileAsync(DriveId, "借过 - 印子月.flac", stream, ParentFileId);
        Assert.IsTrue(!string.IsNullOrEmpty(res.FileId));
    }*/

    [Test]
    public async Task GetVideoPreviewInfoAsync()
    {
        var res = await _client.GetVideoPreviewPlayInfoAsync(DriveId, "614ffe5c030b1a077c1e4281bfe952ed2df27472");
        Assert.IsTrue(res.VideoPreviewPlayInfo.LiveTranscodingTaskList.Length > 0);
    }

    [Test]
    public async Task GetAudioPlayInfoAsync()
    {
        var res = await _client.GetAudioPlayInfoAsync(DriveId, "619631496d7ce70a72d349c7a734c7122fe31ebf");
        Assert.IsTrue(!string.IsNullOrEmpty(res.TemplateList.FirstOrDefault()?.Url));
    }

    [Test]
    public async Task GetOfficePreviewUrlAsync()
    {
        var res = await _client.GetOfficePreviewUrlAsync(DriveId, "619b0d23d93f391246c5472ca2ee9986c6480218");
        Assert.IsTrue(!string.IsNullOrEmpty(res.PreviewUrl));
    }
}