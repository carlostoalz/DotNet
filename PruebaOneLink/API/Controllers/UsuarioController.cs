
namespace API.Controllers
{
    using BE.Bases;
    using BE.Resultado;
    using BE.Seguridad;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Negocio;
    using System;

    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioBusiness>
    {
        #region Constructor
        public UsuarioController(IConfiguration configuration) : base(configuration)
        {
            this.Business = new UsuarioBusiness(configuration);
        }
        #endregion

        [HttpPost]
        [Route("Login")]
        public RSV_Global<UserToken> Login(UserInfo user)
        {
            RSV_Global<UserToken> infoResultado = new RSV_Global<UserToken>();

            try
            {
                infoResultado.Datos = this.Business.Login(user);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 200;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }

        [HttpPost]
        [Route("Registrar")]
        public RSV_Global<bool> Registrar([FromBody]Usuario usuario)
        {
            RSV_Global<bool> infoResultado = new RSV_Global<bool>();

            try
            {
                infoResultado.Datos = this.Business.RegistrarUsuario(usuario);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 201;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }
    }
}