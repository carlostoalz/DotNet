namespace Daserva.API.Controllers
{
    using System;
    using Daserva.Business.Providers;
    using Daserva.Common.Bases;
    using Daserva.Common.Resultado;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("api/Login")]
    public partial class LoginController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public LoginController(IConfiguration config)
        {
            this.Configuration = config;
        }

        [HttpPost]
        [Route("Registro")]
        public RSV_Resultado<bool> Registro([FromBody] RegistroDTO registro)
        {
            LoginProvider provider = null;
            RSV_Resultado<bool> infoResultado = new RSV_Resultado<bool>();
            try
            {
                provider = new LoginProvider(this.Configuration);
                infoResultado.Datos = provider.Registro(registro);
                infoResultado.Exitoso = true;
            }
            catch (Exception ex)
            {
                infoResultado.Exitoso = false;
                infoResultado.Error = new Error(ex, $"Se presentó un error en el método {((System.Reflection.MethodInfo)(System.Reflection.MethodBase.GetCurrentMethod())).Name.ToString()}, {ex.Message}");
            }
            finally
            {
                provider = null;
            }
            return infoResultado;
        }

        [HttpPost]
        [Route("Validar")]
        public RSV_Resultado<Usuario> LoginValidar([FromBody] Usuario login)
        {
            LoginProvider provider = null;
            RSV_Resultado<Usuario> infoResultado = new RSV_Resultado<Usuario>();
            try
            {
                provider = new LoginProvider(this.Configuration);
                infoResultado.Datos = provider.LoginValidar(login);
                infoResultado.Exitoso = true;
            }
            catch (Exception ex)
            {
                infoResultado.Exitoso = false;
                infoResultado.Error = new Error(ex, $"Se presentó un error en el método {((System.Reflection.MethodInfo)(System.Reflection.MethodBase.GetCurrentMethod())).Name.ToString()}, {ex.Message}");
            }
            finally
            {
                provider = null;
            }
            return infoResultado;
        }
    }
}
