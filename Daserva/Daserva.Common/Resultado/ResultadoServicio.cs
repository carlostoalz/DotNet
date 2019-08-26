namespace Daserva.Common.Resultado
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RSV_Resultado<T> : Resultado
    {
        private T _datos;

        public T Datos
        {
            get { return _datos; }
            set { _datos = value; }
        }
    }
}
