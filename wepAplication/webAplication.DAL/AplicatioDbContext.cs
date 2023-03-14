using Microsoft.EntityFrameworkCore;
using wepAplication;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Domain.Accounts;
using webAplication.Persons;
using webAplication.Models;
using webAplication.DAL.models;

namespace webAplication.DAL;
/// <summary>
/// This class usses for interaction with data base
/// </summary>
public class AplicationDbContext : DbContext
{
    public DbSet<MenuEntity> Menus { get; set; }
    public DbSet<DishEntity> Dishes { get; set; }
    public DbSet<MenuEntity> Menuse { get; set; }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PersonEntity> Person { get; set; }
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<TrusteeEntity> Trustees { get; set; }
    public DbSet<SchoolKidEntity> SchoolKids { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<FileModelEntity> Files { get; set; }
    public DbSet<ClassEntity> Classes { get; set; }
    public DbSet<SchoolKidAttendanceEntity> Attendances { get; set; }
    public DbSet<CanteenEmployeeEntity> CanteenEmployees { get; set; }
  /*  public DbSet<ParentAccountEntity> ParentAccounts { get; set; }*/

    public DbSet<TeacherEntity> Teachers { get; set; }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        if (Users.Count() == 0)
        {
          /*  var user = new UserEntity();*//*new AdminEntity("admin", "string"), "string");*//*

            Users.AddAsync(user);*/


/*            var trusteePerson = new TrusteeEntity("trustee", "Andrew");
            var trustee = new UserEntity(trusteePerson, "Andrew");*/

/*            Users.AddAsync(trustee);
*/
            SaveChanges();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .HasKey(order => order.id)
            .HasName("PK_OrderId");

        modelBuilder.Entity<SchoolKidAttendanceEntity>()
            .HasKey(at => at.schoolKidId)
            .HasName("PK_SchoolKidAttendanceId");

        modelBuilder.Entity<UserEntity>(e =>
        {
            e.Property<string>("Login").HasColumnName("Login");
            e.Property<string>("Password").HasColumnName("Password");
            e.HasKey(d => d.Id).HasName("PK_UserId");
        });

 /*       modelBuilder.Entity<ParentAccount>()
            .HasKey(pa => pa.Id)
            .HasName("PK_ParentAccountId");*/

        modelBuilder.Entity<DishMenuEntity>()
    .HasKey(t => new { t.dishId, t.menuId});

        modelBuilder.Entity<DishMenuEntity>()
            .HasOne(dm => dm.dish)
            .WithMany(d => d.dishMenus)
            .HasForeignKey(dm => dm.dishId);

        modelBuilder.Entity<DishMenuEntity>()
            .HasOne(dm => dm.menu)
            .WithMany(m => m.dishMenus)
            .HasForeignKey(dm => dm.menuId);

        modelBuilder.Entity<SchoolKidAttendanceEntity>()
            .HasKey(k => k.schoolKidId)
            .HasName("Id");


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}