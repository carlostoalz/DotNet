namespace Datos.Context
{
    using BE.Bases;
    using BE.Seguridad;
    using Datos.Helpers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class UsuarioContext : BaseContext
    {
        #region Constructor
        public UsuarioContext(IConfiguration configuration) : base(configuration) { }

        public UsuarioContext(string connectionString) : base(connectionString) { }
        #endregion

        #region Procedimientos
        public bool usp_Usuarios_Login(UserInfo user)
        {
            bool wLoginCorrecto = false;
            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@NombreUsuario", SqlDbType.NVarChar, 200, user.NombreUsuario),
                Parameter.NewParameter("@Contrasena", SqlDbType.NVarChar, 200, user.Contrasena)
            };

            DbCommand command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Usuarios_Login]";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            wLoginCorrecto = (bool)command.ExecuteScalar();
            command.Connection.Close();

            return wLoginCorrecto;
        }

        public bool usp_Usuarios_Actualizar(Usuario usuario)
        {
            bool resultado = false;

            List<DbParameter> parameters = new List<DbParameter>()
            {
                Parameter.NewParameter("@NombreUsuario", SqlDbType.NVarChar, 200, usuario.NombreUsuario),
                Parameter.NewParameter("@Contrasena", SqlDbType.NVarChar, 200, usuario.Contrasena),
                Parameter.NewParameter("@Nombre", SqlDbType.NVarChar, 1000, usuario.Nombre),
                Parameter.NewParameter("@Email", SqlDbType.NVarChar, 1000, usuario.Email),
            };

            DbCommand command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_Usuarios_Actualizar]";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            resultado = (decimal)command.ExecuteScalar() > 0;
            command.Connection.Close();

            return resultado;
        }
        #endregion
    }
}
