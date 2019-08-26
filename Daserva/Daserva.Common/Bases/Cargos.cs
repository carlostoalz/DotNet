using System;
using System.Collections.Generic;

namespace Daserva.Common.Bases
{
    public partial class Cargos
    {
        public Cargos()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
