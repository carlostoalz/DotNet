using System;
using System.Collections.Generic;
using System.Text;

namespace Daserva.Common.Resultado
{
    public class Resultado
    {
        private bool blnExitoso = false;
        private Error exException = null;
        private int intCantidadPaginas = 1;

        public bool Exitoso
        {
            get { return blnExitoso; }
            set { blnExitoso = value; }
        }

        public int CantidadPaginas
        {
            get { return intCantidadPaginas; }
            set { intCantidadPaginas = value; }
        }

        public Error Error
        {
            get { return exException; }
            set { exException = value; }
        }
    }
}
