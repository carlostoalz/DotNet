using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BE.Request
{
    public class PropertyRequest
    {
        public IEnumerable<IFormFile> Images { get; set; }
        public string Property { get; set; }
        public bool SaveImagesInDB { get; set; }
    }
}
