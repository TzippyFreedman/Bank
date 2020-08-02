namespace UserService.Helpers.Interfaces
{
    public interface IPasswordHasher
    {
        string CreatePasswordHash(string value, string salt);
        string CreateSalt();
        bool VerifyPassword(string value, string salt, string hash);
    }
}