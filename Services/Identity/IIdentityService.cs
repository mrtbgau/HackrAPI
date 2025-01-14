
namespace API.Services.Identity
{
    public interface IIdentityService
    {
        Task<object> GenerateIdentityAsync();
    }
}