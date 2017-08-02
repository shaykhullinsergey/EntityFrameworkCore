using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WithJsonConfiguration
{
  public class Person
  {
    public int Sex { get; set; }
    public int FullAge { get; set; }
    public string Name { get; set; }

    public void Test()
    {
      var query = GetTable()
        .Where(p => p.FullAge > 18 && p.Sex == 0)
        .GroupBy(p => p.Name)
        .Select(x => $"{x.Key} : {x.Count()}");

    }

    public IEnumerable<Person> GetTable()
    {
      decimal dm = 3.5m;
      uint ui = 100u;
      ulong ul = 100ul;
      double d = 100d;

      return null;
    }
  }


  class Program
  {
    static void Main(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      
      var connectionString = config.GetConnectionString("DefaultConnection");

      var options = new DbContextOptionsBuilder<UserContext>()
        .UseSqlServer(connectionString)
        .Options;

      using (var context = new UserContext(options))
      {
        foreach (var user in context.Users.ToList())
        {
          Console.WriteLine($"> Name: {user.Name}, Age: {user.Age}");
        }
      }
    }
  }
}
