using Microsoft.EntityFrameworkCore;

namespace WithJsonConfiguration
{
  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
  }
}
