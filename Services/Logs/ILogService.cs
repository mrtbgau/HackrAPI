using API.Models;

namespace API.Services.Logs
{
    public interface ILogService
    {
        void LogAction(int userId, string action, string details);
        Task<IEnumerable<Log>> GetRecentLogs(int count);
        Task<IEnumerable<Log>> GetUserLogs(int userId, int count);
    }
}
