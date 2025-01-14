
namespace API.Services.Password
{
    public interface IPasswordService
    {
        string GenerateSecurePassword();
        Task<bool> IsWeakPasswordAsync(string password);
    }
}