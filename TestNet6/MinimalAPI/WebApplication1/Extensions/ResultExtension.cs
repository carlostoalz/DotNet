using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace Presentacion.Extensions
{
    internal static class ResultsExtensions
    {
        public static IResult ResultResponse<T>(this IResultExtensions resultExtensions, T data, string returnMessage, bool isSuccess)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));
            return new Result<T>(data, returnMessage, isSuccess);
        }
    }

    class Result<T> : IResult
    {
        public T data { get; set; }
        public bool isSuccess { get; set; }
        public string returnMessage { get; set; }

        public Result(T Data, string ReturnMessage, bool IsSuccess)
        {
            this.data = Data;
            this.isSuccess = IsSuccess;
            this.returnMessage = ReturnMessage;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            string content = JsonConvert.SerializeObject(this);
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
            switch (httpContext.Request.Method)
            {
                case "GET":
                case "PUT":
                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    break;
                case "POST":
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                    break;
            }
            return httpContext.Response.WriteAsync(content);
        }
    }
}
