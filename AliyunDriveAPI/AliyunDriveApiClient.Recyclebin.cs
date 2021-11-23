using System.Text.Json.Nodes;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    public async Task<FileListResponse> RecyclebinListAsync(RecyclebinListRequest request)
       => await SendJsonPostAsync<FileListResponse>("v2/recyclebin/list", request);

    public async Task MoveToRecyclebin(string driveId, string fileId)
        => await MoveToRecyclebin(new() { DriveId = driveId, FileId = fileId});

    public async Task MoveToRecyclebin(FileBaseRequest request)
        => await SendJsonPostAsync("v2/recyclebin/trash", request);

    public async Task RestoreFromRecyclebin(string driveId, string fileId)
       => await RestoreFromRecyclebin(new() { DriveId = driveId, FileId = fileId });

    public async Task RestoreFromRecyclebin(FileBaseRequest request)
        => await SendJsonPostAsync("v2/recyclebin/restore", request);

    public async Task<ClearRecyclebinResponse> ClearRecyclebinAsync(string driveId)
        => await SendJsonPostAsync<ClearRecyclebinResponse>("v2/recyclebin/clear", new JsonObject { ["drive_id"] = driveId });
}