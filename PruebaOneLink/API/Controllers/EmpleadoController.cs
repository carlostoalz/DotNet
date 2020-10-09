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

    [Route("api/Empleado")]
    [ApiController]
    public class EmpleadoController : BaseController<EmpleadoBussiness>, IController<Empleado>
    {
        #region Constructor
        public EmpleadoController(IConfiguration configuration) : base(configuration)
        {
            this.Business = new EmpleadoBussiness(configuration);
        }
        #endregion

        [HttpGet]
        [Route("Obtener")]
        public RSV_Global<List<Empleado>> ObtenerEmpelados(int pPaginaActual = 1, int pTamanoPagina = 10, string ptermino=null)
        {
            RSV_Global<List<Empleado>> infoResultado = new RSV_Global<List<Empleado>>();
            int cantidadPaginas = 0;
            try
            {
                infoResultado.Datos = this.Business.ObtenerEmpleados(ref cantidadPaginas, pPaginaActual, pTamanoPagina, ptermino);
                infoResultado.CatidadPaginas = cantidadPaginas;
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
        public RSV_Global<Empleado> Seleccionar(decimal id)
        {
            RSV_Global<Empleado> infoResultado = new RSV_Global<Empleado>();

            try
            {
                infoResultado.Datos = this.Business.SeleccionarEmpleado(id);
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
        public RSV_Global<Empleado> Actualizar(Empleado entity)
        {
            RSV_Global<Empleado> infoResultado = new RSV_Global<Empleado>();

            try
            {
                infoResultado.Datos = this.Business.ActualizarEmpleado(entity);
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
                infoResultado.Datos = this.Business.EliminarEmpleado(id);
                infoResultado.Exitoso = true;
                infoResultado.CodigoRespuesta = 200;
            }
            catch (Exception ex)
            {
                infoResultado = this.SetError(infoResultado, ex);
            }

            return infoResultado;
        }

        public RSV_Global<List<Empleado>> Obtener()
        {
            throw new NotImplementedException();
        }
    }
}