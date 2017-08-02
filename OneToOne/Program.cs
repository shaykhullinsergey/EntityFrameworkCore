using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OneToOne
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new UserContextFactory().CreateDbContext(args))
      {
        context.GetService<ILoggerFactory>().AddProvider(new LoggerProvider());

        Create();
        Edit();
        Remove();

        void Create()
        {
          var user1 = new User { Login = "login1", Password = "password1" };
          var user2 = new User { Login = "login2", Password = "password2" };
          context.Users.AddRange(user1, user2);
          context.SaveChanges();

          var profile1 = new UserProfile { Age = 22, Name = "Tom", UserId = user1.Id };
          var profile2 = new UserProfile { Age = 23, Name = "Jill", UserId = user2.Id };
          context.UserProfiles.AddRange(profile1, profile2);
          context.SaveChanges();

          Display();
        }

        void Edit()
        {
          var user = context.Users.Include(u => u.Profile).FirstOrDefault();
          if (user != null)
          {
            user.Password = "secret222";
            context.SaveChanges();
          }

          if (user.Profile != null)
          {
            user.Profile.Name = "Alice II";
            context.UserProfiles.Update(user.Profile);
          }

          Display();
        }

        void Remove()
        {
          var user = context.Users.Include(u => u.Profile).FirstOrDefault();
          if(user != null)
          {
            context.Users.Remove(user);
            context.SaveChanges();
          }

          var profile = context.UserProfiles.FirstOrDefault(x => x.UserId == user.Id);
          if(profile != null)
          {
            context.UserProfiles.Remove(profile);
            context.SaveChanges();
          }

          Display();
        }

        void Display()
        {
          var users = context.Users.Include(user => user.Profile).ToList();
          foreach (var user in users)
          {
            Console.WriteLine($"Name: {user.Profile.Name}, Age: {user.Profile.Age}, Login: {user.Profile.Age}, Password: {user.Password}");
          }
        }
      }
    }
  }
}
