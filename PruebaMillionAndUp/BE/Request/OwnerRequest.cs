using Microsoft.AspNetCore.Http;

namespace BE.Request
{
    public class OwnerRequest
    {
        public IFormFile PhotoFile { get; set; }
        public string Owner { get; set; }
    }
}
