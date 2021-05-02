using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PruebaMillionAndUp.Controllers
{
    public abstract class BaseController<T>: ControllerBase
    {
        public BaseController(IConfiguration configuration) => this._configuration = configuration;

        private readonly IConfiguration _configuration;

        protected IConfiguration Configuration
        {
            get { return _configuration; }
        }

        protected T Domain { get; set; }
    }
}
