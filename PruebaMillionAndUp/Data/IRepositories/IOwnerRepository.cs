using BE.Bases;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IOwnerRepository
    {
        public Task<Owner> InsertOwner(Owner owner);

        public Task<Owner> GetOwner(string email);
    }
}
