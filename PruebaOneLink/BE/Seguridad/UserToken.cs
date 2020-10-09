namespace BE.Seguridad
{

    using System;

    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
