using API.Models;

namespace API.Services.Logs
{
    public interface ILogService
    {
        Task LogActionAsync(int userId, string action, string details);
        Task<IEnumerable<Log>> GetRecentLogsAsync(int count);
        Task<IEnumerable<Log>> GetUserLogsAsync(int userId, int count);
    }
}
