namespace Negocio
{
    using BE.Bases;
    using Datos.Context;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class SubAreaBusiness : BaseBusiness<SubAreaContext>
    {
        #region Constructor
        public SubAreaBusiness(IConfiguration configuration) : base(configuration)
        {
            this.Context = new SubAreaContext(configuration);
        }
        public SubAreaBusiness(string connectionString) : base(connectionString)
        {
            this.Context = new SubAreaContext(connectionString);
        }
        #endregion

        #region Métodos Públicos
        public List<SubArea> ObtenerSubAreas()
        {
            return this.Context.usp_SubAreas_Obtener();
        }

        public SubArea SeleccionarSubArea(decimal pIdSubArea)
        {
            return this.Context.usp_SubAreas_Seleccionar(pIdSubArea);
        }

        public List<SubArea> BuscarSubAreas(decimal pIdArea)
        {
            return this.Context.usp_SubAreas_Buscar(pIdArea);
        }

        public SubArea ActualizarSubArea(SubArea pSubArea)
        {
            return this.Context.usp_SubAreas_Actualizar(pSubArea);
        }

        public bool EliminarSubArea(decimal pIdSubArea)
        {
            return this.Context.usp_SubAreas_Eliminar(pIdSubArea);
        }
        #endregion
    }
}
