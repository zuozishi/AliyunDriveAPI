using AliyunDriveAPI.Models.Converters;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    private string _refreshToken;
    private string _token;
    private DateTime? _tokenExpireTime;

    private readonly HttpClient _httpClient;

    public static JsonSerializerOptions JsonSerializerOptions
        => new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            Converters =
            {
                new Models.Converters.JsonStringEnumConverter(),
                new JsonNodeConverter(),
                new TimeSpanSecondConverter(),
                new NullableTimeSpanSecondConverter()
            }
        };

    public AliyunDriveApiClient(string refreshToken)
    {
        _refreshToken = refreshToken;
        _httpClient = new() { BaseAddress = new Uri("https://api.aliyundrive.com/") };
    }

    public async Task<RefreshTokenResponse> RefreshTokenAsync()
    {
        var obj = new JsonObject
        {
            ["refresh_token"] = _refreshToken
        };
        return await SendJsonPostAsync<RefreshTokenResponse>("https://websv.aliyundrive.com/token/refresh", obj, false);
    }

    private bool IsTokenExpire() => _tokenExpireTime == null || _tokenExpireTime.Value > DateTime.UtcNow;

    private async Task PrepareToken()
    {
        if (!IsTokenExpire()) return;
        var res = await RefreshTokenAsync();
        _token = res.AccessToken;
        _refreshToken = res.RefreshToken;
        _tokenExpireTime = res.ExpireTime;
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
    }

    private async Task<T> SendJsonPostAsync<T>(string url, JsonNode obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        if(prepareToken)
            await PrepareToken();
        var content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        var json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }

    private async Task<T> SendJsonPostAsync<T>(string url, object obj, bool prepareToken = true)
    {
        if (url == null)
            throw new ArgumentNullException(nameof(url));
        if (prepareToken)
            await PrepareToken();
        string body = obj == null ? "{}" : JsonSerializer.Serialize(obj, JsonSerializerOptions);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var resp = await _httpClient.PostAsync(url, content);
        string json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }
}