using BE.Bases;
using BE.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IDomain
{
    public interface IPropertyDomain
    {
        public Task<Property> InsertProperty(PropertyRequest propertyRequest, int IdOwner, string path);
        public Task<Property> GetProperty(int propertyId);
        public Task<IEnumerable<Property>> GetProperties();
    }
}
