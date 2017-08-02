using Microsoft.EntityFrameworkCore;

namespace CRUDOperations
{
  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=userdb;Trusted_connection=True;");
    }
  }
}
