using System;
using System.Collections.Generic;
using System.Text;

namespace Daserva.Common.Resultado
{
    public class Error
    {
        #region Variables

        private string _mensajeUsuario;
        private string _mensaje;
        private string _tipoError;
        private string _fuente;
        private string _pila;
        private string _mensajeInnerException;

        #endregion

        #region Constructor
        public Error()
        {

        }

        public Error(Exception exception, string MensajeUsuario)
        {
            this.MensajeUsuario = MensajeUsuario;
            this.Mensaje = exception.ToString();
            this.Fuente = exception.Source;
            this.Pila = exception.StackTrace;
            if (exception.InnerException != null)
            {
                this.MensajeInnerException = exception.InnerException.Message;
            }
        }
        #endregion

        #region Propiedades


        public string MensajeUsuario
        {
            get { return _mensajeUsuario; }
            set { _mensajeUsuario = value; }
        }


        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }


        public string TipoError
        {
            get { return _tipoError; }
            set { _tipoError = value; }
        }


        public string Fuente
        {
            get { return _fuente; }
            set { _fuente = value; }
        }


        public string Pila
        {
            get { return _pila; }
            set { _pila = value; }
        }


        public string MensajeInnerException
        {
            get { return _mensajeInnerException; }
            set { _mensajeInnerException = value; }
        }

        #endregion
    }
}
