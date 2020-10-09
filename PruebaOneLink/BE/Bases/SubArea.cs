namespace BE.Bases
{
    using System;
    public class SubArea
    {
        public decimal IdSubArea { get; set; }
        public string NombreSubArea { get; set; }
        public decimal IdArea { get; set; }
        public decimal UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
