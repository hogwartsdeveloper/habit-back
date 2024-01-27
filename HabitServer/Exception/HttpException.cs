using System.Net;

namespace HabitServer.Exception;

public class HttpException : System.Exception
{
    public HttpException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
    
    public HttpStatusCode StatusCode { get; set; }
}