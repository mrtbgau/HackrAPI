using API.Models;

namespace API.Services.Logs
{
    public interface ILogService
    {
        void LogAction(int userId, string details);
        List<Log> GetRecentLogs(int count);
        List<Log> GetUserLogs(int userId, int count);
    }
}
