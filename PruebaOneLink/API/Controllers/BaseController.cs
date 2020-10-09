namespace API.Controllers
{
    using BE.Resultado;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;

    public abstract class BaseController<T> : ControllerBase
    {
        #region Propiedades
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        protected IConfiguration Configuration
        {
            get { return _configuration; }
        }

        protected string ConnectionString
        {
            get { return _connectionString; }
        }

        protected T Business { get; set; }
        #endregion

        #region Constructor
        public BaseController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public BaseController(string connectionString)
        {
            this._connectionString = connectionString;
        }
        #endregion

        #region Métodos Protegidos
        protected RSV_Global<U> SetError<U>(RSV_Global<U> infoResultado, Exception ex)
        {
            infoResultado.Exitoso = false;
            infoResultado.Error = new Error(ex.Message, ex.StackTrace);
            infoResultado.CodigoRespuesta = 500;

            return infoResultado;
        }
        #endregion
    }
}
