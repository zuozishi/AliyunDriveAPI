using AliyunDriveAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace AliyunDriveAPI;

public partial class AliyunDriveApiClient
{
    private Dictionary<string, Type> _exceptionTypesDic;

    private async Task<string> TryThrowExceptionAndReadContentAsync(string url, HttpResponseMessage resp)
    {
        if (resp.StatusCode == System.Net.HttpStatusCode.NoContent)
            return "";
        string json = await resp.Content.ReadAsStringAsync();
        var obj = JsonNode.Parse(json);
        string code = (string)obj["code"];
        string message = (string)obj["message"];
        if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(message))
            return json;
        var ex = new APIException(message)
        {
            Url = url,
            Headers = new(resp.Headers.ToDictionary(x => x.Key, x => x.Value.First())),
            StatusCode = resp.StatusCode,
            Code = code,
            ResponseContent = json
        };
        throw GetTypedException(ex);
    }

    private APIException GetTypedException(APIException ex)
    {
        if (_exceptionTypesDic == null)
            _exceptionTypesDic = new(new KeyValuePair<string, Type>[]
            {
                new ("NotFound.", typeof(NotFoundException)),
                new ("JsonParseException", typeof(JsonParseException)),
                new ("BadRequest", typeof(BadRequestException)),
                new ("InvalidParameter.", typeof(BadRequestException)),
                new ("ForbiddenNoPermission.", typeof(ForbiddenNoPermissionException)),
                new ("AccessTokenInvalid", typeof(AccessTokenInvalidException)),
                new ("InvalidResource.", typeof(InvalidResourceException)),
                new ("AlreadyExist.", typeof(AlreadyExistException))
            });
        if (ex.Code == null)
            return ex;
        var type = _exceptionTypesDic.Where(o => ex.Code.StartsWith(o.Key)).Select(o => o.Value).FirstOrDefault();
        if (type == null)
            return ex;
        var newEx = (APIException)type.Assembly.CreateInstance(type.FullName, true, System.Reflection.BindingFlags.Default, null, new object[] { ex }, null, null);
        return newEx;
    }
}