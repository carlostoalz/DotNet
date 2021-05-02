using BE.Bases;
using BE.Request;
using Data.IRepositories;
using Data.Repositories;
using Domain.IDomain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class PropertyDomain : BaseDomain<IPropertyRepository>, IPropertyDomain
    {
        private readonly IConfiguration _configuration;
        public PropertyDomain(IConfiguration configuration) 
        {
            this._configuration = configuration;
            this.Repositoty = new PropertyRepository(configuration); 
        }
        public async Task<Property> InsertProperty(PropertyRequest propertyRequest, int IdOwner, string path)
        {
            Property property = JsonConvert.DeserializeObject<Property>(propertyRequest.Property);
            property.IdOwner = IdOwner;

            if (propertyRequest.Images != null && propertyRequest.Images.ToList().Count > 0)
            {
                List<PropertyImage> images = new List<PropertyImage>();

                if (propertyRequest.SaveImagesInDB)
                {
                    foreach (var image in propertyRequest.Images)
                    {
                        using MemoryStream stream = new();
                        image.CopyTo(stream);
                        byte[] bytes = stream.ToArray();
                        images.Add(new() { 
                            File = Convert.ToBase64String(bytes), 
                            Enabled = true 
                        });
                    }
                }
                else
                {
                    string uploads = Path.Combine(path, this._configuration["folderimages"]);
                    foreach (var image in propertyRequest.Images)
                    {
                        string filePath = Path.Combine(uploads, image.FileName);
                        using Stream fileStrem = new FileStream(filePath, FileMode.Create);
                        await image.CopyToAsync(fileStrem);
                        images.Add(new()
                        {
                            File = filePath,
                            Enabled = true
                        });                        
                    }
                }
                property.PropertyImages = images;
            }

            return await this.Repositoty.InsertProperty(property);
        }
        public async Task<Property> GetProperty(int propertyId) => await this.Repositoty.GetProperty(propertyId);
        public async Task<IEnumerable<Property>> GetProperties()
        {
            PropertyTable propertyTable = await this.Repositoty.GetProperties();

            propertyTable.Properties.ToList().ForEach(property =>
            {
                property.PropertyImages = propertyTable.PropertyImages.Where(p => p.IdProperty == property.IdProperty);
                property.PropertyTraces = propertyTable.PropertyTraces.Where(p => p.IdProperty == property.IdProperty);
            });

            return propertyTable.Properties;
        }
    }
}
