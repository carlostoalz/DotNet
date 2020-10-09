namespace Daserva.Data.Models
{
    using Daserva.Common;
    using Daserva.Common.Bases;
    using Daserva.Data.Helpers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    public partial class DaservaContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _config;
        private string _connectionString;

        public DaservaContext(IConfiguration config)
        {
            _config = config;
        }

        public DaservaContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DaservaContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (this._config != null)
                {
                    optionsBuilder.UseSqlServer(this._config.GetConnectionString("DaservaConnection"));
                }
                else if (this._connectionString != null)
                {
                    optionsBuilder.UseSqlServer(this._connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");
        }

        //Procedures
        #region Seguridad
        public long usp_Usuarios_Actualizar(Usuario usuario, string strCodigoPerfil)
        {
            long lngIDUsuario = -1;
            List<DbParameter> parameters = new List<DbParameter>()
            {
                ParameterHelper.NewParameter("@lngID",SqlDbType.BigInt,8,usuario.lngID),
                ParameterHelper.NewParameter("@strUsuario",SqlDbType.NVarChar,100,usuario.strUsuario),
                ParameterHelper.NewParameter("@strPassword",SqlDbType.NVarChar,100,usuario.strPassword),
                ParameterHelper.NewParameter("@lngIDCliente",SqlDbType.BigInt,8,usuario.lngIDCliente),
                ParameterHelper.NewParameter("@strCodigoPerfil",SqlDbType.NVarChar,4,strCodigoPerfil)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "seguridad.usp_Usuarios_Actualizar";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            lngIDUsuario = (long)command.ExecuteScalar();
            command.Connection.Close();
            return lngIDUsuario;
        }

        public Usuario usp_Login_Validar(Usuario login)
        {
            Usuario usuario = null;
            List<DbParameter> parameters = new List<DbParameter>()
            {
                ParameterHelper.NewParameter("@strlogin",SqlDbType.NVarChar,100,login.strUsuario),
                ParameterHelper.NewParameter("@strPassword",SqlDbType.NVarChar,100,login.strPassword)
            };

            DbCommand command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "seguridad.usp_Login_Validar";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader != null)
                {
                    usuario = reader.Translate<Usuario>().FirstOrDefault();
                }
            }
            command.Connection.Close();
            return usuario;
        }
        #endregion

        #region Clientes
        public long usp_Clientes_Actualizar(Cliente cliente)
        {
            long lngIDCliente = -1;
            List<DbParameter> parameters = new List<DbParameter>()
            {
                ParameterHelper.NewParameter("@lngID", SqlDbType.BigInt, 8, cliente.lngID),
                ParameterHelper.NewParameter("@strCodigoTipoDocumento", SqlDbType.NVarChar, 4, cliente.strCodigoTipoDocumento),
                ParameterHelper.NewParameter("@strNumeroDocumento", SqlDbType.NVarChar,200,cliente.strNumeroDocumento),
                ParameterHelper.NewParameter("@strNombre", SqlDbType.NVarChar,1020,cliente.strNombre),
                ParameterHelper.NewParameter("@strApellidos", SqlDbType.NVarChar,1020,cliente.strApellidos),
                ParameterHelper.NewParameter("@strDireccion", SqlDbType.NVarChar,1020,cliente.strDireccion),
                ParameterHelper.NewParameter("@strComuna", SqlDbType.NVarChar,1020,cliente.strComuna),
                ParameterHelper.NewParameter("@strTelMovil", SqlDbType.NVarChar,200,cliente.strTelMovil),
                ParameterHelper.NewParameter("@strTelParticular", SqlDbType.NVarChar,200,cliente.strTelParticular),
                ParameterHelper.NewParameter("@strTelTrabajo", SqlDbType.NVarChar,200,cliente.strTelTrabajo),
                ParameterHelper.NewParameter("@strEmail", SqlDbType.NVarChar,400,cliente.strEmail),
                ParameterHelper.NewParameter("@dtmFechaNacimiento", SqlDbType.SmallDateTime,4,cliente.dtmFechaNacimiento),
                ParameterHelper.NewParameter("@logActivo", SqlDbType.Bit,1,cliente.logActivo),
                ParameterHelper.NewParameter("@strUsuario", SqlDbType.NVarChar,200,cliente.strUsuarioCreacion)
            };

            var command = this.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "clientes.usp_Clientes_Actualizar";
            command.Parameters.AddRange(parameters.ToArray());
            command.Connection.Open();
            lngIDCliente = (long)command.ExecuteScalar();
            command.Connection.Close();
            return lngIDCliente;
        }
        #endregion
    }
}
