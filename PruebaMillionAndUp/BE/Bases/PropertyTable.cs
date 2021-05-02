using System.Collections.Generic;

namespace BE.Bases
{
    public class PropertyTable
    {
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<PropertyImage> PropertyImages { get; set; }
        public IEnumerable<PropertyTrace> PropertyTraces { get; set; }
    }
}
