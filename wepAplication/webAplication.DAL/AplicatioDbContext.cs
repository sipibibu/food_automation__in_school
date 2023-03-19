using Microsoft.EntityFrameworkCore;
using webAplication.Domain;
using webAplication.Domain.Persons;

namespace webAplication.DAL;
/// <summary>
/// This class usses for interaction with data base
/// </summary>
public class AplicationDbContext : DbContext
{
    public DbSet<User.Entity> Users { get; set; }
    public DbSet<Admin.Entity> Admins { get; set; }
    public DbSet<Person.Entity> Person { get; set; }
    public DbSet<Parent.Entity> Trustees { get; set; }
    public DbSet<Teacher.Entity> Teachers { get; set; }
    public DbSet<SchoolKid.Entity> SchoolKids { get; set; }
    public DbSet<CanteenEmployee.Entity> CanteenEmployees { get; set; }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
       /* if (Users.Count() == 0)
        {
            var user = new UserEntity(); new AdminEntity("admin", "string"), "string");

            Users.AddAsync(user);


            var trusteePerson = new TrusteeEntity("trustee", "Andrew");
            var trustee = new UserEntity(trusteePerson, "Andrew");

            Users.AddAsync(trustee);

        }*/
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

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}