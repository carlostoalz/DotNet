using BE.Bases;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IPropertyRepository
    {
        public Task<Property> InsertProperty(Property property);
        public Task<Property> GetProperty(int propertyId);
        public Task<PropertyTable> GetProperties();
    }
}
