namespace Negocio
{
    using BE.Bases;
    using Datos.Context;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    
    public class EmpleadoBussiness : BaseBusiness<EmpleadoContext>
    {
        #region Constructor
        public EmpleadoBussiness(IConfiguration configuration) : base(configuration)
        {
            this.Context = new EmpleadoContext(configuration);
        }
        public EmpleadoBussiness(string connectionString) : base(connectionString)
        {
            this.Context = new EmpleadoContext(connectionString);
        }
        #endregion

        #region Métodos Públicos
        public List<Empleado> ObtenerEmpleados(ref int pCantidadPaginas, int pPaginaActual = 1, int pTamanoPagina = 10, string ptermino = null)
        {
            return this.Context.usp_Empleados_Obtener(ref pCantidadPaginas, pPaginaActual, pTamanoPagina, ptermino);
        }

        public Empleado SeleccionarEmpleado(decimal pIdEmmpleado)
        {
            return this.Context.usp_Empleados_Seleccionar(pIdEmmpleado);
        }

        public Empleado ActualizarEmpleado(Empleado pEmpleado)
        {
            return this.Context.usp_Empleados_Actualizar(pEmpleado);
        }

        public bool EliminarEmpleado(decimal pIdEmmpleado)
        {
            return this.Context.usp_Empleados_Eliminar(pIdEmmpleado);
        }
        #endregion
    }
}
