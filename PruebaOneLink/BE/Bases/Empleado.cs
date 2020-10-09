namespace BE.Bases
{
    using System;
    public class Empleado
    {
        public decimal IdEmmpleado { get; set; }
        public decimal IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public decimal IdArea { get; set; }
        public string NombreArea { get; set; }
        public decimal IdSubArea { get; set; }
        public string NombreSubArea { get; set; }
        public decimal UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
