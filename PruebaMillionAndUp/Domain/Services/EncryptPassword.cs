
namespace Domain.Services
{
    public static class EncryptPassword
    {
        public static string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool ValidatePassword(string hashedPassword, string Password) => BCrypt.Net.BCrypt.Verify(Password, hashedPassword);
    }
}
