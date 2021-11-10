using BE;
using System.Data;

namespace Infraestructure
{
    public class UtilRepository : GenericRepository, IUtilRepository
    {
        private readonly GlobalAppSettings _settings;
        public UtilRepository(GlobalAppSettings settings): base(settings.Settings.DbConnection) => this._settings = settings;
        public async Task<IEnumerable<Timezone>> GetTimezones() => await this.GetAsync<Timezone>(this._settings.Settings.Procedures.GetTimezones, null, CommandType.StoredProcedure);
    }
}
