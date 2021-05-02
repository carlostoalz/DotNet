using BE.Bases;
using BE.RSV;
using Domain;
using Domain.IDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace PruebaMillionAndUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController<IAuthDomain>
    {
        public AuthController(IConfiguration configuration) : base(configuration) 
        {
            this.Domain = new AuthDomain(configuration,new OwnerDomain(configuration));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<RSV<string>>> Login([FromBody] Owner owner)
        {
            RSV<string> infoResultado = new();

            try
            {
                string token = await this.Domain.Login(owner);

                if (!string.IsNullOrEmpty(token))
                {
                    infoResultado.Datos = token;
                    infoResultado.Exitoso = true;
                    infoResultado.StatusCode = 200;
                }
                else
                {
                    infoResultado.Exitoso = false;
                    infoResultado.StatusCode = 401;
                    infoResultado.Error = new(new("El correo o contraseña que envio no es la correcta, intente de nuevo"));
                }
            }
            catch (Exception ex)
            {
                infoResultado.Exitoso = false;
                infoResultado.Error = new(ex);
                infoResultado.StatusCode = 500;
            }

            return infoResultado;
        }
    }
}
