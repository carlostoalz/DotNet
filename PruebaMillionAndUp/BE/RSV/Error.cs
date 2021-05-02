using System;

namespace BE.RSV
{
    public class Error
    {
        public Error(Exception exception)
        {
            this.Mensaje = exception.Message;
            this.Pila = exception.StackTrace;
        }

        public string Mensaje { get; set; }
        public string Pila { get; set; }
    }
}
