namespace Datos.Context
{
    using BE.Bases;
    using Datos.Helpers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    public class EmpleadoContext : BaseContext
    {
        #region Constructor
        public EmpleadoContext(IConfiguration configuration) : base(configuration) { }

        public EmpleadoContext(string connectionString) : base(connectionString) { }
        #endregion

        #region Procedimientos
        public List<Empleado> usp_Empleados_Obtener(ref int pCantidadPaginas, int pPaginaActual = 1, int pTamanoPagina = 10, string ptermino = null)
        {
            List<Empleado> empleados = null;

            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@PaginaActual", SqlDbType.Int, 4, pPaginaActual),
                Parameter.NewParameter("@TamanoPagina", SqlDbType.Int, 4, pTamanoPagina),
                Parameter.NewParameter("@termino", SqlDbType.NVarChar, 1000, ptermino),
                Parameter.NewParameter("@cantPaginas", SqlDbType.Int, 4, pCantidadPaginas, ParameterDirection.Output)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Empleados_Obtener]";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    empleados = reader.Translate<Empleado>().ToList();
                    pCantidadPaginas =  int.Parse(parameters.Where(p => p.ParameterName == "@cantPaginas").Select(p => p.Value).FirstOrDefault().ToString());
                }
            }
            command.Connection.Close();

            return empleados;
        }

        public Empleado usp_Empleados_Seleccionar(decimal pIdEmmpleado)
        {
            Empleado empleado = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Empleados_Seleccionar]";
            command.Parameters.Add(Parameter.NewParameter("@IdEmmpleado", SqlDbType.Int, 9, pIdEmmpleado));
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    empleado = reader.Translate<Empleado>().FirstOrDefault();
                }
            }
            command.Connection.Close();

            return empleado;
        }

        public Empleado usp_Empleados_Actualizar(Empleado pEmpleado)
        {
            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@IdEmmpleado",SqlDbType.Int,9,pEmpleado.IdEmmpleado),
                Parameter.NewParameter("@IdTipoDocumento",SqlDbType.Int,9,pEmpleado.IdTipoDocumento),
                Parameter.NewParameter("@NumeroDocumento",SqlDbType.NVarChar,100,pEmpleado.NumeroDocumento),
                Parameter.NewParameter("@Nombres",SqlDbType.NVarChar,1000,pEmpleado.Nombres),
                Parameter.NewParameter("@Apellidos",SqlDbType.NVarChar,1000,pEmpleado.Apellidos),
                Parameter.NewParameter("@IdArea",SqlDbType.Int,9,pEmpleado.IdArea),
                Parameter.NewParameter("@IdSubArea",SqlDbType.Int,9,pEmpleado.IdSubArea),
                Parameter.NewParameter("@Usuario",SqlDbType.Int,9,pEmpleado.UsuarioCreacion)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Empleados_Actualizar]";
            command.Parameters.AddRange(parameters.ToArray());

            command.Connection.Open();
            pEmpleado.IdEmmpleado = (decimal)command.ExecuteScalar();
            command.Connection.Close();

            return pEmpleado;
        }

        public bool usp_Empleados_Eliminar(decimal pIdEmmpleado)
        {
            bool resultado = false;
            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Empleados_Eliminar]";
            command.Parameters.Add(Parameter.NewParameter("@IdEmpleado", SqlDbType.Int, 9, pIdEmmpleado));
            command.Connection.Open();
            resultado = command.ExecuteNonQuery() > 0;
            command.Connection.Close();
            return resultado;
        }
        #endregion
    }
}
