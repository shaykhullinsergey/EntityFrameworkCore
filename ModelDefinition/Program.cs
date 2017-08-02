using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDefinition
{
  public class Phone
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Company Manufacturer { get; set; }
  }

  //[NotMapped]
  public class Company
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }

  //[Table("Tablets")]
  public class Tablet
  {
    //[Column("user_id")]
    //[Key]
    public int Id { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Name { get; set; }
    //[DefaultValue(18)]
    public int Price { get; set; }
  }

  public class ApplicationContext : DbContext
  {
    public DbSet<Phone> Phones { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      //Включить в модель планшет
      builder.Entity<Tablet>();

      //Исключить из модели компанию
      builder.Ignore<Company>();

      //Для планшетов будет создана таблица Tablets
      builder.Entity<Tablet>().ToTable("Tablets");

      //Для свойства Id будет спользована колонка user_id
      builder.Entity<Tablet>().Property(t => t.Id).HasColumnName("user_id");

      //Для основного ключа вместо Id, TabletId будет использоваться Price
      builder.Entity<Tablet>().HasKey(t => t.Price);

      //Основной ключ будет составным из Id и Name
      builder.Entity<Tablet>().HasKey(t => new { t.Id, t.Name });

      //Основным ключом будет Id, который в колонке будет с названием CustomUserId
      builder.Entity<Tablet>().HasKey(t => t.Id).HasName("CustomUserId");

      //Колонка Price будет уникальной
      builder.Entity<Tablet>().HasAlternateKey(t => t.Price);

      //Колонка Price будет создана с индексом для ускорения
      builder.Entity<Phone>().HasIndex(p => p.Price);

      //Помимо ускорения колонка должна быть еще и уникальной
      builder.Entity<Phone>().HasIndex(p => p.Price).IsUnique();

      //Свойство Name должно быть обязательно задано на клиенте
      builder.Entity<Tablet>().Property(t => t.Name).ValueGeneratedNever();

      //Если цена не задана, она будет равна 18
      builder.Entity<Tablet>().Property(t => t.Price).HasDefaultValue(18);

      //Обновить токен может только один человек
      builder.Entity<Tablet>().Property(t => t.Name).IsConcurrencyToken();
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new ApplicationDbFactory().CreateDbContext(args))
      {

      }
    }
  }
}
