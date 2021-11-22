namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    public async Task<VideoPreviewInfoResponse> GetVideoPreviewPlayInfoAsync(string driveId, string fileId, string category = "live_transcoding", VideoPreviewTemplateType templateId = VideoPreviewTemplateType.NONE)
        => await GetVideoPreviewPlayInfoAsync(new()
        {
            DriveId = driveId,
            FileId = fileId,
            Category = category,
            TemplateId = templateId
        });

    public async Task<VideoPreviewInfoResponse> GetVideoPreviewPlayInfoAsync(VideoPreviewInfoRequest request)
        => await SendJsonPostAsync<VideoPreviewInfoResponse>("v2/file/get_video_preview_play_info", request);

    public async Task<AudioPlayInfoResponse> GetAudioPlayInfoAsync(string driveId, string fileId)
        => await GetAudioPlayInfoAsync(new(driveId, fileId));

    public async Task<AudioPlayInfoResponse> GetAudioPlayInfoAsync(FileBaseRequest request)
        => await SendJsonPostAsync<AudioPlayInfoResponse>("v2/databox/get_audio_play_info", request);

    public async Task<OfficePreviewUrlResponse> GetOfficePreviewUrlAsync(string driveId, string fileId)
        => await GetOfficePreviewUrlAsync(new(driveId, fileId));

    public async Task<OfficePreviewUrlResponse> GetOfficePreviewUrlAsync(FileBaseRequest request)
        => await SendJsonPostAsync<OfficePreviewUrlResponse>("v2/file/get_office_preview_url", request);
}