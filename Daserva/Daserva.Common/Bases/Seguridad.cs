using System;
using System.Collections.Generic;

namespace Daserva.Common.Bases
{
    public partial class Usuario
    {
        public long lngID { get; set; }
        public string strUsuario { get; set; }
        public string strPassword { get; set; }
        public long lngIDCliente { get; set; }
        public long lngIDPerfil { get; set; }
    }

    public partial class Perfil
    {
        public long lngID { get; set; }
        public string strCodigo { get; set; }
        public string strNombre { get; set; }
        public string strDescripcion { get; set; }
    }
}
