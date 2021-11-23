using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    private async Task<T> SendJsonPostAsync<T>(string url, JsonNode obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        if (prepareToken)
            await PrepareTokenAsync();
        var content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        var json = await TryThrowExceptionAndReadContentAsync(url, resp);
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }

    private async Task SendJsonPostAsync(string url, JsonNode obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        if (prepareToken)
            await PrepareTokenAsync();
        var content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        await TryThrowExceptionAndReadContentAsync(url, resp);
    }

    private async Task<T> SendJsonPostAsync<T>(string url, object obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (prepareToken)
            await PrepareTokenAsync();
        string body = obj == null ? "{}" : JsonSerializer.Serialize(obj, JsonSerializerOptions);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        var json = await TryThrowExceptionAndReadContentAsync(url, resp);
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }

    private async Task SendJsonPostAsync(string url, object obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (prepareToken)
            await PrepareTokenAsync();
        string body = obj == null ? "{}" : JsonSerializer.Serialize(obj, JsonSerializerOptions);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        await TryThrowExceptionAndReadContentAsync(url, resp);
    }
}