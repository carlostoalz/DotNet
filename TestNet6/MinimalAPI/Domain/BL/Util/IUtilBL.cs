using BE;

namespace Domain
{
    public interface IUtilBL
    {
        Task<IEnumerable<Timezone>> GetTimezones();
    }
}
