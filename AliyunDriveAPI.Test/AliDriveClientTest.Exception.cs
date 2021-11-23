using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace AliyunDriveAPI.Test;

public partial class AliDriveClientTest
{
    [Test]
    public async Task FileNotFoundExceptionTest()
    {
        try
        {
            await _client.FileGetAsync(DriveId, "test");
            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is Exceptions.NotFoundException);
        }
    }
}