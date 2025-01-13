
namespace API.Services.Mail
{
    public interface IMailService
    {
        Task<bool> VerifyEmailExistenceAsync(string email);
    }
}