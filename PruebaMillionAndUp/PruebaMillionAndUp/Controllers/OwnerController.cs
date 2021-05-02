using BE.Bases;
using BE.Request;
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
    public class OwnerController : BaseController<IOwnerDomain>
    {
        public OwnerController(IConfiguration configuration) : base(configuration) 
        {
            this.Domain = new OwnerDomain(configuration);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        public async Task<ActionResult<RSV<Owner>>> InsertOwner([FromForm] OwnerRequest ownerRequest)
        {
            RSV<Owner> infoResultado = new();

            try
            {
                infoResultado.Datos = await this.Domain.InsertOwner(ownerRequest);
                infoResultado.Exitoso = true;
                infoResultado.StatusCode = 201;
            }
            catch (Exception ex)
            {
                infoResultado.Exitoso = false;
                infoResultado.Error = new Error(ex);
                infoResultado.StatusCode = 500;
            }

            return infoResultado;
        }
    }
}
