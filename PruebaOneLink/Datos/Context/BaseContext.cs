
namespace Datos.Context
{
    using BE.ContextBE;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public abstract class BaseContext : IdentityDbContext<AppContext>
    {
        #region Propiedades
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        protected string ConnectionString
        {
            get { return this._connectionString; }
        }

        protected IConfiguration Configuration
        {
            get { return this._configuration; }
        }
        #endregion

        #region Constructor
        public BaseContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public BaseContext(string connectionString)
        {
            this._connectionString = connectionString;
        } 
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (this.Configuration != null)
                {
                    optionsBuilder.UseSqlServer(this.Configuration.GetConnectionString("Empleados"));
                } 
                else if (this.ConnectionString != null)
                {
                    optionsBuilder.UseSqlServer(this.ConnectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");
        }
    }
}
