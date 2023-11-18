using System.Net;

namespace Domain.Wrappers;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public Response(T data)
    {
        StatusCode = 200;
        Data = data;
    }

    public Response(HttpStatusCode code,string message)
    {
        StatusCode = (int)code;
        Errors.Add(message);
    }

    public Response(HttpStatusCode code) => StatusCode = (int)code;
}