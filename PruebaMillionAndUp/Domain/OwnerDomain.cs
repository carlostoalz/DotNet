using BE.Bases;
using BE.Request;
using Data.IRepositories;
using Data.Repositories;
using Domain.IDomain;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Domain
{
    public class OwnerDomain : BaseDomain<IOwnerRepository>, IOwnerDomain
    {
        public OwnerDomain(IConfiguration configuration) => this.Repositoty = new OwnerRepository(configuration);

        public async Task<Owner> InsertOwner(OwnerRequest ownerRequest)
        {
            Owner owner = JsonConvert.DeserializeObject<Owner>(ownerRequest.Owner, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            owner.Passord = EncryptPassword.Encrypt(owner.Passord);

            if (ownerRequest.PhotoFile != null)
            {
                using MemoryStream stream = new();
                ownerRequest.PhotoFile.CopyTo(stream);
                byte[] bytes = stream.ToArray();
                owner.Photo = Convert.ToBase64String(bytes);
            }
            else
            {
                owner.Photo = string.Empty;
            }

            owner = await this.Repositoty.InsertOwner(owner);
            owner.Passord = string.Empty;
            return owner;
        }

        public async Task<Owner> GetOwner(string email) => await this.Repositoty.GetOwner(email);
    }
}
