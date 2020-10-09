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

    public class SubAreaContext : BaseContext
    {
        #region Constructor
        public SubAreaContext(IConfiguration configuration) : base(configuration){}
        public SubAreaContext(string connectionString) : base(connectionString){ }
        #endregion

        #region Procedimientos
        public List<SubArea> usp_SubAreas_Obtener()
        {
            List<SubArea> subAreas = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_SubAreas_Obtener]";
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    subAreas = reader.Translate<SubArea>().ToList();
                }
            }
            command.Connection.Close();

            return subAreas;
        }

        public SubArea usp_SubAreas_Seleccionar(decimal pIdSubArea)
        {
            SubArea subArea = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_SubAreas_Seleccionar]";
            command.Parameters.Add(Parameter.NewParameter("@IdSubArea", SqlDbType.Int, 9, pIdSubArea));
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    subArea = reader.Translate<SubArea>().FirstOrDefault();
                }
            }
            command.Connection.Close();

            return subArea;
        }

        public List<SubArea> usp_SubAreas_Buscar(decimal pIdArea)
        {
            List<SubArea> subAreas = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_SubAreas_Buscar]";
            command.Parameters.Add(Parameter.NewParameter("@IdArea", SqlDbType.Int, 9, pIdArea));
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    subAreas = reader.Translate<SubArea>().ToList();
                }
            }
            command.Connection.Close();

            return subAreas;
        }

        public SubArea usp_SubAreas_Actualizar(SubArea pSubArea)
        {
            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@IdSubArea",SqlDbType.Int,9,pSubArea.IdSubArea),
                Parameter.NewParameter("@NombreSubArea",SqlDbType.NVarChar,400,pSubArea.NombreSubArea),
                Parameter.NewParameter("@IdArea",SqlDbType.Int,9,pSubArea.IdArea),
                Parameter.NewParameter("@Usuario",SqlDbType.Int,9,pSubArea.UsuarioCreacion)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_SubAreas_Actualizar]";
            command.Parameters.AddRange(parameters.ToArray());

            command.Connection.Open();
            pSubArea.IdSubArea = (decimal)command.ExecuteScalar();
            command.Connection.Close();

            return pSubArea;
        }

        public bool usp_SubAreas_Eliminar(decimal pIdSubArea)
        {
            bool resultado = false;
            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_SubAreas_Eliminar]";
            command.Parameters.Add(Parameter.NewParameter("@IdSubArea", SqlDbType.Int, 9, pIdSubArea));
            command.Connection.Open();
            resultado = command.ExecuteNonQuery() > 0;
            command.Connection.Close();
            return resultado;
        }
        #endregion
    }
}
