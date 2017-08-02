using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExistingDatabase
{
  public partial class UserDbContext : DbContext
  {
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=userdb;Trusted_connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
  }
}