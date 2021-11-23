using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace AliyunDriveAPI.Test;

public partial class AliDriveClientTest
{
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