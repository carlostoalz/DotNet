using BE.Bases;
using BE.Request;
using BE.RSV;
using Domain;
using Domain.IDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaMillionAndUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : BaseController<IPropertyDomain>
    {
        private readonly IWebHostEnvironment _environment;
        public PropertyController(
            IConfiguration configuration,
            IWebHostEnvironment environment
        ) : base(configuration)
        {
            this._environment = environment;
            this.Domain = new PropertyDomain(configuration);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        public async Task<ActionResult<RSV<Property>>> InsertProperty([FromForm] PropertyRequest propertyRequest)
        {
            RSV<Property> infoResultado = new();

            try
            {
                int IdOwner = int.Parse(HttpContext.User.Identity.Name);

                if (this._environment != null)
                    infoResultado.Datos = await this.Domain.InsertProperty(propertyRequest, IdOwner, this._environment.ContentRootPath);
                else
                    infoResultado.Datos = await this.Domain.InsertProperty(propertyRequest, IdOwner, @"D:\DesarrolloSoftware\DotNet\PruebaMillionAndUp\PruebaMillionAndUp\"); // Modicar la ruta cuando se ejecuta en pruebas unitarias, para la carpeta que desee

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

        [HttpGet]
        [Route("{propertyId}")]
        public async Task<ActionResult<RSV<Property>>> GetProperty(int propertyId)
        {
            RSV<Property> infoResultado = new();

            try
            {
                infoResultado.Datos = await this.Domain.GetProperty(propertyId);
                infoResultado.Exitoso = true;
                infoResultado.StatusCode = 200;
            }
            catch (Exception ex)
            {
                infoResultado.Exitoso = false;
                infoResultado.Error = new Error(ex);
                infoResultado.StatusCode = 500;
            }

            return infoResultado;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<RSV<IEnumerable<Property>>>> GetProperties()
        {
            RSV<IEnumerable<Property>> infoResultado = new();

            try
            {
                infoResultado.Datos = await this.Domain.GetProperties();
                infoResultado.Exitoso = true;
                infoResultado.StatusCode = 200;
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
