using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OneToOne
{
  public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
  {
    public UserContext CreateDbContext(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var options = new DbContextOptionsBuilder<UserContext>()
        .UseSqlServer(config.GetConnectionString("DefaultConnection"))
        .Options;

      return new UserContext(options);
    }
  }
}
