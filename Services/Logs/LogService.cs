using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Logs
{
    public class LogService(DbAPIContext dbAPIContext) : ILogService
    {
        private readonly DbAPIContext dbAPIContext = dbAPIContext;

        public void LogAction(int userId, string details)
        {
            Log newLog = new() { 
                Date = DateTime.Now,
                UserId = userId,
                Details = details
            };

            dbAPIContext.Logs.Add(newLog);
            dbAPIContext.SaveChanges();
        }
        
        public List<Log> GetRecentLogs(int count)
        {
            return dbAPIContext.Logs.OrderByDescending(l => l.Date).Take(count).ToList();
        }

        public List<Log> GetUserLogs(int userId, int count)
        {
            return dbAPIContext.Logs.OrderByDescending(log => log.Date).Take(count).Where(user => user.UserId == userId).ToList();
        }
    }
}
