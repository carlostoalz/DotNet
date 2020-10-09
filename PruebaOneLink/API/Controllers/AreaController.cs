namespace API.Controllers
{
    using BE.Bases;
    using BE.Interfaces;
    using BE.Resultado;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Negocio;
    using System;
    using System.Collections.Generic;

    [Route("api/Area")]
    [ApiController]
    public class AreaController : BaseController<AreaBusiness>, IController<Area>
    {
        #region Constructor
        public AreaController(IConfiguration configuration) : base(configuration)
        {
            this.Business = new AreaBusiness(configuration);
        }
        #endregion

        [HttpGet]
        [Route("Obtener")]
        public RSV_Global<List<Area>> Obtener()
        {
            RSV_Global<List<Area>> infoResultado = new RSV_Global<List<Area>>();

            try
            {
                infoResultado.Datos = this.Business.ObtenerAreas();
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
        public RSV_Global<Area> Seleccionar(decimal id)
        {
            RSV_Global<Area> infoResultado = new RSV_Global<Area>();

            try
            {
                infoResultado.Datos = this.Business.SeleccionarArea(id);
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
        public RSV_Global<Area> Actualizar(Area entity)
        {
            RSV_Global<Area> infoResultado = new RSV_Global<Area>();

            try
            {
                infoResultado.Datos = this.Business.ActualizarArea(entity);
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
                infoResultado.Datos = this.Business.EliminarArea(id);
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