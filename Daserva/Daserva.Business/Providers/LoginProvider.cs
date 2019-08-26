namespace Daserva.Business.Providers
{
    using Daserva.Common.Bases;
    using Daserva.Data.Models;
    using Microsoft.Extensions.Configuration;

    public class LoginProvider
    {
        private IConfiguration configuration;
        private string _connectionString;

        public LoginProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public LoginProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Registro(RegistroDTO registro)
        {
            bool result = false;
            long intCliente = 0;
            long intUsuario = 0;
            using (DaservaContext db = new DaservaContext(this.configuration))
            {
                intCliente = db.usp_Clientes_Actualizar(registro.Cliente);
                if (intCliente > 0)
                {
                    registro.Usuario.lngIDCliente = intCliente;
                    intUsuario = db.usp_Usuarios_Actualizar(registro.Usuario, registro.Perfil.strCodigo);
                }
            }

            if (intCliente > 0 && intUsuario > 0)
            {
                result = true;
            }

            return result;
        }

        public Usuario LoginValidar(Usuario login)
        {
            using (DaservaContext db = new DaservaContext(this.configuration))
            {
                return db.usp_Login_Validar(login);
            }
        }
    }
}
