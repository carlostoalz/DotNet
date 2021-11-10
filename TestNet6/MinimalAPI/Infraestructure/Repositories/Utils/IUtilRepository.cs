using BE;

namespace Infraestructure
{
    public interface IUtilRepository
    {
        Task<IEnumerable<Timezone>> GetTimezones();
    }
}
