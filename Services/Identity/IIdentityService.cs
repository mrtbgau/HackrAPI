
namespace API.Services.Identity
{
    public interface IIdentityService
    {
        Task<object> GenerateIdentityAsync();
        Task<object> SearchPersonAsync(string firstName, string lastName);
    }
}