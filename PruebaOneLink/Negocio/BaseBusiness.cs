namespace Negocio
{
    using Microsoft.Extensions.Configuration;

    public abstract class BaseBusiness<T>
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

        protected T Context { get; set; }
        #endregion

        #region Constructor
        public BaseBusiness(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public BaseBusiness(string connectionString)
        {
            this._connectionString = connectionString;
        } 
        #endregion
    }
}
