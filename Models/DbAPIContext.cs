using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class DbAPIContext(DbContextOptions<DbAPIContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
}