using System;
using System.Linq;

namespace ExistingDatabase
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new UserDbContext())
      {
        int index = 1;
        foreach (var user in context.Users.ToList())
        {
          Console.WriteLine($"<{index++}> Name: {user.Name}, Age: {user.Age}");
        }
      }
    }
  }
}
