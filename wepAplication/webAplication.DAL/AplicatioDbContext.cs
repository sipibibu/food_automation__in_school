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
    public DbSet<BuffetMenu.Entity> BuffetMenus { get; set; }
    public DbSet<User.Entity> Users { get; set; }
    public DbSet<Admin.Entity> Admins { get; set; }
    public DbSet<Person.Entity> Person { get; set; }
    public DbSet<Parent.Entity> Parents { get; set; }
    public DbSet<Teacher.Entity> Teachers { get; set; }
    public DbSet<SchoolKid.Entity> SchoolKids { get; set; }
    public DbSet<CanteenEmployee.Entity> CanteenEmployees { get; set; }
    public DbSet<SchoolKidAttendance.Entity> Attendances { get; set; }
    public DbSet<Class.Entity> Classes { get; set; }
    public DbSet<Order.Entity> Orders { get; set; }
    public DbSet<FileModel.Entity> Files { get; set; }
    public DbSet<EmailVerification.Entity> EmailVerification { get; set; }


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
            .HasDiscriminator<string>("Type")
            .HasValue<Menu.Entity>("Menu.Entity")
            .HasValue<BuffetMenu.Entity>("BuffetMenu.Entity");
        
        
        modelBuilder
            .Entity<Menu.Entity>()
            .HasMany(m => m.Dishes)
            .WithMany(d => d.Menus)
            .UsingEntity<DishMenu.Entity>(x => x
                    .HasOne(dm => dm.Dish)
                    .WithMany(d => d.DishMenus)
                    .HasForeignKey(dm => dm.DishId),
                j => j
                    .HasOne(dm => dm.Menu)
                    .WithMany(m => m.DishMenus)
                    .HasForeignKey(pt => pt.MenuId),
                j =>
                {
                    j.Property(dm => dm.ServiceDate)
                        .IsRequired(false);
                    j.HasKey(dm => dm.Id);
                    j.ToTable("DishMenus");
                });

        modelBuilder
            .Entity<Parent.Entity>()
            .HasMany(x => x.SchoolKids)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .IsRequired(false);

        modelBuilder
            .Entity<SchoolKid.Entity>()
            .HasOne(k => k.Class)
            .WithMany(x => x.SchoolKids)
            .HasForeignKey(x=>x.ClassId)
            .IsRequired(false);

        modelBuilder.Entity<Order.Entity>()
            .HasOne<Menu.Entity>()
            .WithMany()
            .HasForeignKey(x => x.MenuId);

        modelBuilder.Entity<Order.Entity>()
            .HasMany(x => x.Dishes)
            .WithMany()
            .UsingEntity(x => x.ToTable("OrdersDishes"));
/*        modelBuilder
            .Entity<FileModel.Entity>()
            .HasKey(x => x.Id);*/
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}