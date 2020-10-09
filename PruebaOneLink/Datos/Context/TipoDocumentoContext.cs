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

    public class TipoDocumentoContext : BaseContext
    {
        #region Constructor
        public TipoDocumentoContext(IConfiguration configuration) : base(configuration) { }

        public TipoDocumentoContext(string connectionString) : base(connectionString) { }
        #endregion

        #region Procedimientos
        public List<TipoDocumento> usp_TiposDocumento_Obtener()
        {
            List<TipoDocumento> tiposDocumento = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_TiposDocumento_Obtener]";
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    tiposDocumento = reader.Translate<TipoDocumento>().ToList();
                }
            }
            command.Connection.Close();

            return tiposDocumento;
        }

        public TipoDocumento usp_TiposDocumento_Seleccionar(decimal pIdTipoDocumento)
        {
            TipoDocumento tipoDocumento = null;

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_TiposDocumento_Seleccionar]";
            command.Parameters.Add(Parameter.NewParameter("@IdTipoDocumento", SqlDbType.Int, 9, pIdTipoDocumento));
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    tipoDocumento = reader.Translate<TipoDocumento>().FirstOrDefault();
                }
            }
            command.Connection.Close();

            return tipoDocumento;
        }

        public TipoDocumento usp_TiposDocumento_Actualizar(TipoDocumento pTipoDocumento)
        {
            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@IdTipoDocumento",SqlDbType.Int,9,pTipoDocumento.IdTipoDocumento),
                Parameter.NewParameter("@NombreTipoDocumento",SqlDbType.NVarChar,400,pTipoDocumento.NombreTipoDocumento),
                Parameter.NewParameter("@Usuario",SqlDbType.Int,9,pTipoDocumento.UsuarioCreacion)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_TiposDocumento_Actualizar]";
            command.Parameters.AddRange(parameters.ToArray());

            command.Connection.Open();
            pTipoDocumento.IdTipoDocumento = (decimal)command.ExecuteScalar();
            command.Connection.Close();

            return pTipoDocumento;
        }

        public bool usp_TiposDocumento_Eliminar(decimal pIdTipoDocumento)
        {
            bool resultado = false;
            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_TiposDocumento_Eliminar]";
            command.Parameters.Add(Parameter.NewParameter("@IdTipoDocumento", SqlDbType.Int, 9, pIdTipoDocumento));
            command.Connection.Open();
            resultado = command.ExecuteNonQuery() > 0;
            command.Connection.Close();
            return resultado;
        }
        #endregion
    }
}
