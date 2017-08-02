using Microsoft.EntityFrameworkCore;

namespace OneToOne
{
  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    public UserContext(DbContextOptions<UserContext> options) 
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>()
        .HasOne(u => u.Profile)
        .WithOne(p => p.User)
        .HasForeignKey<UserProfile>(x => x.UserId);
    }
  }
}
