using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Logger
{
  public class MyLoggerProvider : ILoggerProvider
  {
    public ILogger CreateLogger(string categoryName)
    {
      return new MyLogger();
    }

    public void Dispose()
    {
    }
  }

  public class MyLogger : ILogger
  {
    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      File.AppendAllText("log.txt", formatter(state, exception));
      Console.WriteLine(formatter(state, exception));
    }
  }

  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
  }

  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
  }

  public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
  {
    public UserContext CreateDbContext(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var options = new DbContextOptionsBuilder<UserContext>()
        .UseSqlServer(config.GetConnectionString("Default"))
        .Options;

      return new UserContext(options);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new UserContextFactory().CreateDbContext(args))
      {
        context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

        var user1 = new User { Name = "Tom", Age = 33 };
        var user2 = new User { Name = "Alice", Age = 26 };

        context.Users.Add(user1);
        context.Users.Add(user2);
        context.SaveChanges();

        var users = context.Users.ToList();
        Console.WriteLine("Данные после добавления:");
        foreach (var user in users)
        {
          Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
        }
      }
    }
  }
}
