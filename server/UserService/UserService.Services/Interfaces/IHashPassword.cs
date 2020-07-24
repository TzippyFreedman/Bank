namespace UserService.Services
{
    public interface IHashPassword
    {
        string CreatePasswordHash(string value, string salt);
        string CreateSalt();
        bool VerifyPassword(string value, string salt, string hash);
    }
}