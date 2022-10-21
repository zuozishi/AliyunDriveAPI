using AliyunDriveAPI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    public async Task<FileGetResponse> FileGetAsync(string driveId, string fileId, int? limit = 100, int? urlExpireSec = 1600)
        => await FileGetAsync(new() { DriveId = driveId, FileId = fileId, UrlExpireSec = urlExpireSec });

    public async Task<FileListResponse> FileListAsync(FileListRequest request)
        => await SendJsonPostAsync<FileListResponse>("adrive/v3/file/list", request);

    public async Task<FileListResponse> FileSearchAsync(string driveId, SampleFileSearchQuery sampleFileSearchQuery, int? limit = 100, string nextMarker = null, OrderByType? orderBy = OrderByType.UpdatedAt, OrderDirectionType? orderDirection = OrderDirectionType.DESC)
        => await FileSearchAsync(driveId, sampleFileSearchQuery.GetQueryExpression(), limit, nextMarker, orderBy, orderDirection);

    public async Task<FileListResponse> FileSearchAsync(string driveId, FileSearchQueryExpression queryExpression, int? limit = 100, string nextMarker = null, OrderByType? orderBy = OrderByType.UpdatedAt, OrderDirectionType? orderDirection = OrderDirectionType.DESC)
        => await FileSearchAsync(driveId, queryExpression.ToString(), limit, nextMarker, orderBy, orderDirection);

    public async Task<FileListResponse> FileSearchAsync(string driveId, string query, int? limit = 100, string nextMarker = null, OrderByType? orderBy = OrderByType.UpdatedAt, OrderDirectionType? orderDirection = OrderDirectionType.DESC)
        => await FileSearchAsync(new() { DriveId = driveId, Query = query, Limit = limit, Marker = nextMarker, OrderByType = orderBy, OrderDirection = orderDirection });

    public async Task<FileListResponse> FileSearchAsync(FileSearchRequest request)
        => await SendJsonPostAsync<FileListResponse>("adrive/v3/file/search", request);

    public async Task<FileGetResponse> FileGetAsync(FileGetRequest request)
        => await SendJsonPostAsync<FileGetResponse>("v2/file/get", request);

    public async Task<CreateFolderResponse> CreateFolderAsync(string driveId, string name, string parentFileId)
        => await CreateFolderAsync(new() { DriveId = driveId, Name = name, ParentFileId = parentFileId });

    public async Task<CreateFolderResponse> CreateFolderAsync(CreateFolderRequest request)
        => await SendJsonPostAsync<CreateFolderResponse>("adrive/v2/file/createWithFolders", request);

    public async Task<FileGetResponse> UploadFileAsync(string driveId, string fileName, byte[] bytes, string parentFileId, CheckNameModeType mode, int chunkSize = 1024 * 1024 * 10)
    {
        if (driveId == null)
            throw new ArgumentNullException(nameof(driveId));
        if (fileName == null)
            throw new ArgumentNullException(nameof(fileName));
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        if (parentFileId == null)
            throw new ArgumentNullException(nameof(parentFileId));
        await PrepareTokenAsync();
        int chunkCount = (int)Math.Ceiling((double)bytes.Length / chunkSize);
        var preUploadResp = await PreUploadAsync(driveId, fileName, parentFileId, mode, chunkCount);
        for (int i = 0; i < preUploadResp.PartInfoList.Length; i++)
        {
            var partInfo = preUploadResp.PartInfoList[i];
            var partBytes = bytes.Skip(i * chunkSize).Take(chunkSize).ToArray();
            var isOk = await UploadPart(partInfo, partBytes);
            if (!isOk)
                throw new Exception("上传失败");
        }
        return await CompleteUploadAsync(driveId, preUploadResp.FileId, preUploadResp.UploadId);
    }

    public async Task<FileGetResponse> UploadFileAsync(string driveId, string fileName, Stream stream, string parentFileId, CheckNameModeType mode, int chunkSize = 1024 * 1024 * 10)
    {
        if (driveId == null)
            throw new ArgumentNullException(nameof(driveId));
        if (fileName == null)
            throw new ArgumentNullException(nameof(fileName));
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));
        if (parentFileId == null)
            throw new ArgumentNullException(nameof(parentFileId));
        await PrepareTokenAsync();
        int chunkCount = (int)Math.Ceiling((double)stream.Length / chunkSize);
        var preUploadResp = await PreUploadAsync(driveId, fileName, parentFileId, mode, chunkCount);
        var buffer = new byte[chunkSize];
        long remain = stream.Length;
        for (int i = 0; i < preUploadResp.PartInfoList.Length; i++)
        {
            Debug.WriteLine($"正在上传：{i + 1}/{preUploadResp.PartInfoList.Length}");
            var partInfo = preUploadResp.PartInfoList[i];
            stream.Read(buffer, 0, chunkSize);
            var isOk = await UploadPart(partInfo, remain > chunkSize ? buffer : buffer.Take((int)remain).ToArray());
            if (!isOk)
                throw new Exception("上传失败");
            remain -= chunkSize;
        }
        return await CompleteUploadAsync(driveId, preUploadResp.FileId, preUploadResp.UploadId);
    }

    public async Task<PreUploadResponse> PreUploadAsync(string driveId, string name, string parentFileId, CheckNameModeType mode, int partNumber = 1, string hash = null, string hashName = "sha1")
    {
        var partInfos = new List<FileUploadPartInfo>();
        for (int i = 1; i <= partNumber; i++)
            partInfos.Add(new FileUploadPartInfo(i));
        return await PreUploadAsync(new()
        {
            DriveId = driveId,
            Name = name,
            ParentFileId = parentFileId,
            CheckNameMode = mode,
            PartInfoList = partInfos.ToArray(),
            ContentHash = hash,
            ContentHashName = hashName
        });
    }

    public async Task<PreUploadResponse> PreUploadAsync(PreUploadRequest request)
        => await SendJsonPostAsync<PreUploadResponse>("adrive/v2/file/createWithFolders", request);

    public async Task<bool> UploadPart(FileUploadPartInfoWithUrl partInfo, Stream stream, bool useInternalUrl = false)
    {
        if (partInfo == null)
            throw new ArgumentNullException(nameof(partInfo));
        var content = new StreamContent(stream);
        var resp = await _httpClient.PutAsync(useInternalUrl ? partInfo.InternalUploadUrl : partInfo.UploadUrl, content);
        if (resp.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<bool> UploadPart(FileUploadPartInfoWithUrl partInfo, byte[] bytes, bool useInternalUrl = false)
    {
        if (partInfo == null)
            throw new ArgumentNullException(nameof(partInfo));
        var content = new ByteArrayContent(bytes);
        var resp = await _httpClient.PutAsync(useInternalUrl ? partInfo.InternalUploadUrl : partInfo.UploadUrl, content);
        if (resp.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<FileGetResponse> CompleteUploadAsync(string driveId, string fileId, string uploadId)
        => await CompleteUploadAsync(new() { DriveId = driveId, FileId = fileId, UploadId = uploadId });

    public async Task<FileGetResponse> CompleteUploadAsync(CompleteUploadRequest request)
        => await SendJsonPostAsync<FileGetResponse>("v2/file/complete", request);

    public async Task<DownloadUrlResponse> GetDownloadUrlAsync(string driveId, string fileId)
        => await GetDownloadUrlAsync(new() { DriveId = driveId, FileId = fileId });

    public async Task<DownloadUrlResponse> GetDownloadUrlAsync(FileBaseRequest request)
        => await SendJsonPostAsync<DownloadUrlResponse>("v2/file/get_download_url", request);

    public async Task DeleteFileAsync(string driveId, string fileId)
        => await DeleteFileAsync(new() { DriveId = driveId, FileId = fileId });

    public async Task DeleteFileAsync(FileBaseRequest request)
        => await SendJsonPostAsync("v3/file/delete", request);

    public async Task<FileItem> FileRenameAsync(string driveId, string fileId, string name, CheckNameModeType mode = CheckNameModeType.Refuse)
        => await FileRenameAsync(new() { DriveId = driveId, FileId = fileId, Name = name, CheckNameMode = mode });

    public async Task<FileItem> FileRenameAsync(FileRenameRequest request)
        => await SendJsonPostAsync<FileItem>("v3/file/update", request);

    public async Task<FileItem> FileMoveAsync(string driveId, string fileId, string toDriveId, string toParentFileId)
        => await FileMoveAsync(new() { DriveId = driveId, FileId = fileId, ToDriveId = toDriveId, ToParentFileId = toParentFileId });

    public async Task<FileItem> FileMoveAsync(FileMoveRequest request)
        => await SendJsonPostAsync<FileItem>("v3/file/update", request);

    public async Task<FileShareResponse> ShareAsync(FileShareRequest request)
        => await SendJsonPostAsync<FileShareResponse>("adrive/v2/share_link/create", request);

    public async Task<FileShareResponse> ShareAsync(string driveId, string fileId, TimeSpan? expiration = null, string sharePwd = null)
        => await ShareAsync(new FileShareRequest(driveId, fileId, expiration, sharePwd));

    public async Task<FileShareResponse> ShareAsync(string driveId, List<string> fileIdList, TimeSpan? expiration = null, string sharePwd = null)
        => await ShareAsync(new FileShareRequest(driveId, fileIdList, expiration, sharePwd));
}