using Microsoft.EntityFrameworkCore;
using webAplication.DAL.models;
using webAplication.DAL.models.Persons;

namespace webAplication.DAL;

public sealed class AccountDbContext : DbContext
{
    private DbSet<UserEntity> _users { get; set; }
    private DbSet<AdminEntity> _admins { get; set; }
    private DbSet<PersonEntity> _person { get; set; }
    private DbSet<ParentEntity> _trustees { set; get; }
    private DbSet<TeacherEntity> _teachers { get; set; }
    private DbSet<SchoolKidEntity> _schoolKids { get; set; }
    private DbSet<CanteenEmployeeEntity> _canteenEmployees { get; set; }

    public IEnumerable<UserEntity> GetUsers()
    {
        return _users.ToList();
    }
    public void UpdateUser(UserEntity entity)
    {
        
    }

    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        SaveChanges();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}