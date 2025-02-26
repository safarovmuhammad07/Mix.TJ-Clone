using System.Net;

namespace MixTJ_Application.Responses;

public class Response<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public Response(T data)
    {
        Data = data;
        Message = "Success";
        StatusCode = 200;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Message = message;
        Data = default;
    }
    
}