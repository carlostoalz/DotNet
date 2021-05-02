using BE.Bases;
using Domain.IDomain;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Domain
{
    public class AuthDomain : IAuthDomain
    {
        private readonly IConfiguration _configuration;
        private readonly OwnerDomain _ownerDomain;
        public AuthDomain(
            IConfiguration configuration,
            OwnerDomain ownerDomain
        )
        {
            this._configuration = configuration;
            this._ownerDomain = ownerDomain;
        }

        public async Task<string> Login(Owner pOwner)
        {
            Owner owner = await this._ownerDomain.GetOwner(pOwner.Email);
            if (EncryptPassword.ValidatePassword(owner.Passord, pOwner.Passord))
                return JsonWebTokens.Sing(owner.IdOwner, this._configuration["salt"]);
            else            
                return string.Empty;            
        }
    }
}
