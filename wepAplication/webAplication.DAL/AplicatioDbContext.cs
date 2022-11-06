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
class BdSettings{

    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

}
public class AplicationDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Person> Person { get; set; }
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

        var str = o2.ToString();
        //optionsBuilder.UseNpgsql("Host=192.168.1.5;Port=5432;Database=postgres;Username=postgres;Password=password");
        optionsBuilder.UseNpgsql($"Host={o2["Host"]};Port={o2["Port"]};Database={o2["Database"]};Username={o2["Username"]};Password={o2["Password"]}");
    }
}