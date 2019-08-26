namespace Daserva.Common.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Cliente
    {
        public long lngID { get; set; }
        public string strCodigoTipoDocumento { get; set; }
        public string strNumeroDocumento { get; set; }
        public string strNombre { get; set; }
        public string strApellidos { get; set; }
        public string strDireccion { get; set; }
        public string strComuna { get; set; }
        public string strTelMovil { get; set; }
        public string strTelParticular { get; set; }
        public string strTelTrabajo { get; set; }
        public string strEmail { get; set; }
        public DateTime dtmFechaNacimiento { get; set; }
        public bool logActivo { get; set; }
        public string strUsuarioCreacion { get; set; }
        public DateTime dtmFechaCreacion { get; set; }
        public string strUsuarioModificacion { get; set; }
        public DateTime? dtmFechaModificacion { get; set; }
    }
}
