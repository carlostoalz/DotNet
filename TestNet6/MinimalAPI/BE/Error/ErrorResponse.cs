using System;

namespace BE
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string StackTrace { get; set; }
        public int ErrorCode { get; set; }
        public string RequestIP { get; set; }
        public DateTime LogDate { get; set; }
    }
}
