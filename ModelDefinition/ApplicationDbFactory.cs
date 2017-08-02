using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ModelDefinition
{
  public class ApplicationDbFactory : IDesignTimeDbContextFactory<ApplicationContext>
  {
    public ApplicationContext CreateDbContext(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var options = new DbContextOptionsBuilder<ApplicationContext>()
        .UseSqlServer(config.GetConnectionString("DefaultConnection"))
        .Options;

      return new ApplicationContext(options);
    }
  }
}
