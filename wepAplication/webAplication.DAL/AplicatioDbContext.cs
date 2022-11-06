using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Text;
using webAplication.Models;
using wepAplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace webAplication.DAL;
/// <summary>
/// This class usses for interaction with data base
/// </summary>
public class AplicationDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Person> Person { get; set; }
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        if (Users.Count() == 0)
        {
            var user = new User(new Person("admin", "string"), "string");

            Users.AddAsync(user);
            SaveChangesAsync();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>()
            .HasKey(d => d.Id)
            .HasName("PK_DishId");
        modelBuilder.Entity<User>()
            .HasKey(d => d.Id)
            .HasName("PK_UserId");
        modelBuilder.Entity<Person>()
            .HasKey(d => d.Id)
            .HasName("PK_PersonId");

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        JObject o1 = JObject.Parse(File.ReadAllText("..\\webAplication.DAL\\Properties\\dbConnetionSettings.json"));

        JObject o2;
        using (StreamReader file = File.OpenText("..\\webAplication.DAL\\Properties\\dbConnetionSettings.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            o2 = (JObject)JToken.ReadFrom(reader);
        }

        
        optionsBuilder.UseNpgsql($"Host={o2["Host"]};Port={o2["Port"]};Database={o2["Database"]};Username={o2["Username"]};Password={o2["Password"]}");
    }
}