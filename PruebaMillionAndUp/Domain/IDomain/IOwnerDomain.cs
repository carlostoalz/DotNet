using BE.Bases;
using BE.Request;
using System.Threading.Tasks;

namespace Domain.IDomain
{
    public interface IOwnerDomain
    {
        public Task<Owner> InsertOwner(OwnerRequest ownerRequest);
        public Task<Owner> GetOwner(string email);
    }
}
