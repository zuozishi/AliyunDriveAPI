using AliyunDriveAPI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    public async Task<FileListResponse> FileListAsync(FileListRequest request)
        => await SendJsonPostAsync<FileListResponse>("adrive/v3/file/list", request);

    public async Task<FileGetResponse> FileGetAsync(string driveId, string fileId, int? urlExpireSec = 1600)
        => await FileGetAsync(new() { DriveId = driveId, FileId = fileId, UrlExpireSec = urlExpireSec });

    public async Task<FileGetResponse> FileGetAsync(FileGetRequest request)
        => await SendJsonPostAsync<FileGetResponse>("v2/file/get", request);

    public async Task<CreateFolderResponse> CreateFolderAsync(string driveId, string name, string parentFileId = "root")
        => await CreateFolderAsync(new(){ DriveId = driveId, Name = name, ParentFileId = parentFileId });

    public async Task<CreateFolderResponse> CreateFolderAsync(CreateFolderRequest request)
        => await SendJsonPostAsync<CreateFolderResponse>("adrive/v2/file/createWithFolders", request);

    public async Task<FileGetResponse> UploadFileAsync(string driveId, string fileName, byte[] bytes, string parentFileId = "root", int chunkSize = 1024 * 1024 * 10)
    {
        if(driveId == null)
            throw new ArgumentNullException(nameof(driveId));
        if(fileName == null)
            throw new ArgumentNullException(nameof(fileName));
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        if (parentFileId == null)
            throw new ArgumentNullException(nameof(parentFileId));
        int chunkCount = (int)Math.Ceiling((double)bytes.Length / chunkSize);
        var preUploadResp = await PreUploadAsync(driveId, fileName, parentFileId, chunkCount);
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

    public async Task<FileGetResponse> UploadFileAsync(string driveId, string fileName, Stream stream, string parentFileId = "root", int chunkSize = 1024 * 1024 * 10)
    {
        if (driveId == null)
            throw new ArgumentNullException(nameof(driveId));
        if (fileName == null)
            throw new ArgumentNullException(nameof(fileName));
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));
        if (parentFileId == null)
            throw new ArgumentNullException(nameof(parentFileId));
        int chunkCount = (int)Math.Ceiling((double)stream.Length / chunkSize);
        var preUploadResp = await PreUploadAsync(driveId, fileName, parentFileId, chunkCount);
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

    public async Task<PreUploadResponse> PreUploadAsync(string driveId, string name, string parentFileId = "root", int partNumber = 1, string hash = null, string hashName = "sha1")
    {
        var partInfos = new List<FileUploadPartInfo>();
        for (int i = 1; i <= partNumber; i++)
            partInfos.Add(new FileUploadPartInfo(i));
        return await PreUploadAsync(new() {
            DriveId = driveId,
            Name = name,
            ParentFileId = parentFileId,
            PartInfoList = partInfos.ToArray(),
            ContentHash = hash,
            ContentHashName = hashName
        });
    }

    public async Task<PreUploadResponse> PreUploadAsync(PreUploadRequest request)
        => await SendJsonPostAsync<PreUploadResponse>("adrive/v2/file/createWithFolders", request);

    public async Task<bool> UploadPart(FileUploadPartInfoWithUrl partInfo, Stream stream, bool useInternalUrl = false)
    {
        if(partInfo == null)
            throw new ArgumentNullException(nameof(partInfo));
        var content = new StreamContent(stream);
        var resp = await _httpClient.PutAsync(useInternalUrl ? partInfo.InternalUploadUrl : partInfo.UploadUrl, content);
        if(resp.IsSuccessStatusCode)
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
}