using Microsoft.EntityFrameworkCore;
using wepAplication;

namespace webAplication.DAL;

public class AplicationDbContext: DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>()
            .HasKey(d => d.Id)
            .HasName("PK_DishId");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=192.168.1.2;Port=5432;Database=usersdb;Username=postgres;Password=mysecretpassword");
    }
}