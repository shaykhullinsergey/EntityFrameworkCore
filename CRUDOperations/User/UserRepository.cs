using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDOperations
{
  public class UserRepository : IDisposable
  {
    private UserContext context;

    public UserRepository()
    {
      context = new UserContext();
    }

    public async Task AddUserAsync(User user)
    {
      context.Users.Add(user);
      await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
      return await context.Users.ToListAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
      context.Users.Update(user);
      await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
      context.Users.Remove(user);
      await context.SaveChangesAsync();
    }

    public void Dispose()
    {
      context.Dispose();
    }
  }
}
