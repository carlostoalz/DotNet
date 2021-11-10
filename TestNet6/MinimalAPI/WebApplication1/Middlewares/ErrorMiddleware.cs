using BE;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await GetResult(context, exception, HttpStatusCode.Conflict);
            }
        }

        private async Task GetResult(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var route = context.GetRouteData();
            string ControllerName = context.Request.Path.ToString();
            string ActionName = context.Request.Method.ToString();
            context.Response.Clear();
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = @"application/json";
            var error = CreateErrorResponse(exception, context, ControllerName, ActionName);
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error, settings));
        }

        private ErrorResponse CreateErrorResponse(Exception exception, HttpContext context, string ControllerName, string ActionName)
        {
            return new ErrorResponse()
            {
                Message = GetMessage(exception),
                ControllerName = ControllerName,
                ActionName = ActionName,
                StackTrace = GetMessageException(exception),
                ErrorCode = GetCodeException(exception),
                RequestIP = GetRemoteIPAdress(context)
            };
        }

        private string GetMessage(Exception exception)
        {
            try
            {
                ModelBaseException ex = (ModelBaseException)exception;
                return ex.ApplicationMessage.Message;
            }
            catch (Exception)
            {
                return "Not-Message-Defined";
            }
        }

        private string GetRemoteIPAdress(HttpContext context)
        {
            try
            {
                return context.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        private int GetCodeException(Exception exception)
        {
            try
            {
                ModelBaseException ex = (ModelBaseException)exception;
                return ex.ApplicationMessage.Code;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private string GetMessageException(Exception exception, string Message = "")
        {
            if (String.IsNullOrEmpty(Message))
            {
                Message = " Exception:" + exception.Message + ". ";
            }
            else
            {
                Message += exception.Message + ". ";
            }

            if (exception.InnerException != null)
            {
                Message += ". Inner Exception=" + exception.InnerException.Message
                            + ". StackTrace=" + exception.StackTrace;
                Message = GetMessageException(exception.InnerException, Message);
            }
            else
            {
                return Message + ". StackTrace=" + exception.StackTrace;
            }
            return Message;
        }
    }
}
