namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    public async Task<FileListResponse> FileListAsync(FileListRequest request)
        => await SendJsonPostAsync<FileListResponse>("adrive/v3/file/list", request);

    public async Task<VideoPreviewInfoResponse> GetVideoPreviewPlayInfo(VideoPreviewInfoRequest request)
        => await SendJsonPostAsync<VideoPreviewInfoResponse>("v2/file/get_video_preview_play_info", request);
}