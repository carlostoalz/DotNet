namespace BE.Bases
{
    using System;

    public class Area
    {
        public decimal IdArea { get; set; }
        public string NombreArea { get; set; }
        public decimal UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
