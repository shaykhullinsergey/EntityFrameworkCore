using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExistingDatabse
{
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
  }

  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=userdb;Trusted_connection=True;");
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new UserContext())
      {
        //var user = new User
        //{
        //  Name = "Tom",
        //  Age = 55
        //};
        //context.Users.Add(user);
        //context.SaveChanges();

        //var user = context.Users.FirstOrDefault();
        //if (user != null)
        //{
        //  Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
        //}

        //foreach (var user in context.Users.ToList())
        //{
        //  Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
        //}
      }
    }
  }
}
