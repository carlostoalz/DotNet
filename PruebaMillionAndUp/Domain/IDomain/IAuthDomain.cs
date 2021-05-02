using BE.Bases;
using System.Threading.Tasks;

namespace Domain.IDomain
{
    public interface IAuthDomain
    {
        public Task<string> Login(Owner pOwner);
    }
}
