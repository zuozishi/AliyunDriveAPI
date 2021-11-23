using System.Collections.ObjectModel;
using System.Net;

namespace AliyunDriveAPI.Exceptions;

public class APIException : Exception
{
    public string Url { get; set; }

    public ReadOnlyDictionary<string, string> Headers { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public string Code { get; set; }

    public string ResponseContent { get; set; }

    public APIException(string message) : base(message) { }

    public APIException(APIException ex) :base(ex.Message)
    {
        Url = ex.Url;
        Headers = ex.Headers;
        StatusCode = ex.StatusCode;
        Code = ex.Code;
        ResponseContent = ex.ResponseContent;
    }

    public override string ToString() => $"[{Code}] {Message}";
}