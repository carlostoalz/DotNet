using BE;
using Infraestructure;

namespace Domain
{
    public class UtilBL : IUtilBL
    {
        private readonly IUtilRepository _repository;
        public UtilBL(IUtilRepository repository) => this._repository = repository;
        public async Task<IEnumerable<Timezone>> GetTimezones() => await this._repository.GetTimezones();
    }
}
