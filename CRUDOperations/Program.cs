using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperations
{
  class Program
  {
    static void Main(string[] args) 
      => AsyncMain().Wait();

    static async Task AsyncMain()
    {
      using (var repo = new UserRepository())
      {
        await Display("Initial");
        await AddUser();
        await UpdateUser();
        await DeleteUser();
        
        async Task AddUser()
        {
          await repo.AddUserAsync(new User { Name = "Test", Age = 12 });
          await Display("After Add");
        }

        async Task UpdateUser()
        {
          var user = (await repo.GetUsersAsync()).FirstOrDefault();
          user.Name = "AXXXXX";
          await repo.UpdateUserAsync(user);
          await Display("After Update");
        }

        async Task DeleteUser()
        {
          var user = (await repo.GetUsersAsync()).FirstOrDefault();
          await repo.DeleteUserAsync(user);
          await Display("After Delete");
        }

        async Task Display(string message)
        {
          Console.WriteLine(message);

          var users = await repo.GetUsersAsync();
          foreach (var user in users)
          {
            Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
          }
        }
      }
    }
  }
}
