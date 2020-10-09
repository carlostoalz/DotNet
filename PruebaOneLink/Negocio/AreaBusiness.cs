namespace Negocio
{
    using BE.Bases;
    using Datos.Context;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class AreaBusiness: BaseBusiness<AreaContext>
    {
        #region Constructor
        public AreaBusiness(IConfiguration configuration) : base(configuration)
        {
            this.Context = new AreaContext(configuration);
        }
        public AreaBusiness(string connectionString) : base(connectionString)
        {
            this.Context = new AreaContext(connectionString);
        }
        #endregion

        #region Métodos Públicos
        public List<Area> ObtenerAreas()
        {
            return this.Context.usp_Areas_Obtener();
        }

        public Area SeleccionarArea(decimal pIdArea)
        {
            return this.Context.usp_Areas_Seleccionar(pIdArea);
        }

        public Area ActualizarArea(Area pArea)
        {
            return this.Context.usp_Areas_Actualizar(pArea);
        }

        public bool EliminarArea(decimal pIdArea) 
        {
            return this.Context.usp_Areas_Eliminar(pIdArea);
        }
        #endregion
    }
}
