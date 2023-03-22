using Microsoft.EntityFrameworkCore;
using webAplication.Domain;
using webAplication.Domain.Persons;

namespace webAplication.DAL;
/// <summary>
/// This class usses for interaction with data base
/// </summary>
public class AplicationDbContext : DbContext
{
    public DbSet<Dish.Entity> Dishes { get; set; }
    public DbSet<Menu.Entity> Menus { get; set; }
    public DbSet<User.Entity> Users { get; set; }
    public DbSet<Admin.Entity> Admins { get; set; }
    public DbSet<Person.Entity> Person { get; set; }
    public DbSet<Parent.Entity> Parents { get; set; }
    public DbSet<Teacher.Entity> Teachers { get; set; }
    public DbSet<SchoolKid.Entity> SchoolKids { get; set; }
    public DbSet<CanteenEmployee.Entity> CanteenEmployees { get; set; }
    public DbSet<SchoolKidAttendance.Entity> Attendances { get; set; }
    public DbSet<Class.Entity> Classes { get; set; }



    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();

        SaveChanges();
        if (!Users.Any())
        {
            Database.ExecuteSqlRaw("INSERT INTO \"Users\" (\"Id\", \"Login\", \"Password\") VALUES('1', 'string', 'string')");
            Database.ExecuteSqlRaw("INSERT INTO \"Person\" (\"Id\", \"ImageId\", \"Name\", \"Role\", \"UserId\", \"Type\") VALUES('1', 'ajsjda', 'admin', 'admin', '1', 'Admin.Entity')");
        }
        SaveChanges();

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User.Entity>()
            .HasOne(u => u.Person)
            .WithOne(p => p.User)
            .HasForeignKey<Person.Entity>(p => p.UserId);
        
        modelBuilder
            .Entity<Person.Entity>()
            .HasDiscriminator<string>("Type")
            .HasValue<Admin.Entity>("Admin.Entity")
            .HasValue<CanteenEmployee.Entity>("CanteenEmployee.Entity")
            .HasValue<Parent.Entity>("Parent.Entity")
            .HasValue<SchoolKid.Entity>("SchoolKid.Entity")
            .HasValue<Teacher.Entity>("Teacher.Entity");
        
        modelBuilder
            .Entity<Menu.Entity>()
            .HasMany(m => m.Dishes)
            .WithMany(d => d.Menus)
            .UsingEntity(j => j.ToTable("DishMenus"));

        modelBuilder
            .Entity<SchoolKid.Entity>()
            .HasOne(k => k._Class)
            .WithMany(x => x.SchoolKids)
            .HasForeignKey(x=>x.ClassId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}