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

    [Route("api/SubArea")]
    [ApiController]
    public class SubAreaController : BaseController<SubAreaBusiness>, IController<SubArea>
    {
        #region Constructor
        public SubAreaController(IConfiguration configuration) : base(configuration)
        {
            this.Business = new SubAreaBusiness(configuration);
        }
        #endregion

        [HttpGet]
        [Route("Obtener")]
        public RSV_Global<List<SubArea>> Obtener()
        {
            RSV_Global<List<SubArea>> infoResultado = new RSV_Global<List<SubArea>>();

            try
            {
                infoResultado.Datos = this.Business.ObtenerSubAreas();
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
        public RSV_Global<SubArea> Seleccionar(decimal id)
        {
            RSV_Global<SubArea> infoResultado = new RSV_Global<SubArea>();

            try
            {
                infoResultado.Datos = this.Business.SeleccionarSubArea(id);
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
        [Route("Buscar")]
        public RSV_Global<List<SubArea>> Buscar(decimal idArea)
        {
            RSV_Global<List<SubArea>> infoResultado = new RSV_Global<List<SubArea>>();

            try
            {
                infoResultado.Datos = this.Business.BuscarSubAreas(idArea);
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
        public RSV_Global<SubArea> Actualizar([FromBody]SubArea entity)
        {
            RSV_Global<SubArea> infoResultado = new RSV_Global<SubArea>();

            try
            {
                infoResultado.Datos = this.Business.ActualizarSubArea(entity);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 200;
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
                infoResultado.Datos = this.Business.EliminarSubArea(id);
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