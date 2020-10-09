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

    public class AreaContext : BaseContext
    {
        #region Constructor
        public AreaContext(IConfiguration configuration) : base(configuration){}
        public AreaContext(string connectionString) : base(connectionString) { }
        #endregion

        #region Procedimientos
        public List<Area> usp_Areas_Obtener()
        {
            List<Area> areas = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Areas_Obtener]";
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    areas = reader.Translate<Area>().ToList();
                }
            }
            command.Connection.Close();

            return areas;
        }

        public Area usp_Areas_Seleccionar(decimal pIdArea)
        {
            Area area = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Areas_Seleccionar]";
            command.Parameters.Add(Parameter.NewParameter("@IdArea", SqlDbType.Int, 9, pIdArea));
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    area = reader.Translate<Area>().FirstOrDefault();
                }
            }
            command.Connection.Close();

            return area;
        }

        public Area usp_Areas_Actualizar(Area pArea)
        {
            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@IdArea", SqlDbType.Int, 9, pArea.IdArea),
                Parameter.NewParameter("@NombreArea", SqlDbType.NVarChar, 800, pArea.NombreArea),
                Parameter.NewParameter("@Usuario", SqlDbType.Int, 9, pArea.UsuarioCreacion)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Areas_Actualizar]";
            command.Parameters.AddRange(parameters.ToArray());

            command.Connection.Open();
            pArea.IdArea = (decimal)command.ExecuteScalar();
            command.Connection.Close();

            return pArea;
        }

        public bool usp_Areas_Eliminar (decimal pIdArea)
        {
            bool resultado = false;
            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Areas_Eliminar]";
            command.Parameters.Add(Parameter.NewParameter("@IdArea", SqlDbType.Int, 9, pIdArea));
            command.Connection.Open();
            resultado = command.ExecuteNonQuery() > 0;
            command.Connection.Close();
            return resultado;
        }
        #endregion
    }
}
