namespace API.Controllers
{
    using BE.Bases;
    using BE.Interfaces;
    using BE.Resultado;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Negocio;
    using System;
    using System.Collections.Generic;

    [Route("api/TipoDocumento")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoDocumentoController : BaseController<TipoDocumentoBusiness>, IController<TipoDocumento>
    {
        #region Constructor
        public TipoDocumentoController(IConfiguration configuration) : base(configuration)
        {
            this.Business = new TipoDocumentoBusiness(configuration);
        } 
        #endregion

        [HttpGet]
        [Route("Obtener")]
        public RSV_Global<List<TipoDocumento>> Obtener()
        {
            RSV_Global<List<TipoDocumento>> infoResultado = new RSV_Global<List<TipoDocumento>>();

            try
            {
                infoResultado.Datos = this.Business.ObtenerTiposDocumento();
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 200;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }

        [HttpGet]
        [Route("Seleccionar")]
        public RSV_Global<TipoDocumento> Seleccionar(decimal id)
        {
            RSV_Global<TipoDocumento> infoResultado = new RSV_Global<TipoDocumento>();

            try
            {
                infoResultado.Datos = this.Business.SeleccionarTipoDocumento(id);
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
        [Route("Actualizar")]
        public RSV_Global<TipoDocumento> Actualizar([FromBody]TipoDocumento entity)
        {
            RSV_Global<TipoDocumento> infoResultado = new RSV_Global<TipoDocumento>();

            try
            {
                infoResultado.Datos = this.Business.ActualizarTipoDocumento(entity);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 201;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public RSV_Global<bool> Eliminar(decimal id)
        {
            RSV_Global<bool> infoResultado = new RSV_Global<bool>();

            try
            {
                infoResultado.Datos = this.Business.EliminarTipoDocumento(id);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 200;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }
    }
}