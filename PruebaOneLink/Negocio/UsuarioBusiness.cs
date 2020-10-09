
namespace Negocio
{
    using BE.Bases;
    using BE.Seguridad;
    using Datos.Context;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;

    public class UsuarioBusiness : BaseBusiness<UsuarioContext>
    {
        #region Constructor
        public UsuarioBusiness(IConfiguration configuration) : base(configuration)
        {
            this.Context = new UsuarioContext(configuration);
        }
        public UsuarioBusiness(string connectionString) : base(connectionString)
        {
            this.Context = new UsuarioContext(connectionString);
        }
        #endregion

        public UserToken Login(UserInfo user)
        {
            UserToken token = null;
            bool wLoginCorrecto = this.Context.usp_Usuarios_Login(user);

            if (wLoginCorrecto)
            {
                token = this.BuildToken(user);
            }

            return token;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            return this.Context.usp_Usuarios_Actualizar(usuario);
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            Claim[] claims = new[] 
            { 
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.NombreUsuario),
                new Claim("miValor", "Lo que yo quiera"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JWT:key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}