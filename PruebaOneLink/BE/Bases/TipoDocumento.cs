namespace BE.Bases
{
    using System;

    public class TipoDocumento
    {
        public decimal IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public decimal UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
