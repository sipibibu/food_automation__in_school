﻿using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Text;
using wepAplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Persons;

namespace webAplication.DAL;
/// <summary>
/// This class usses for interaction with data base
/// </summary>
public class AplicationDbContext : DbContext
{
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Menu> Menuse { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Trustee> Trustees { get; set; }
    public DbSet<SchoolKid> SchoolKids { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        if (Users.Count() == 0)
        {
            var user = new User(new Admin("admin", "string"), "string");

            Users.AddAsync(user);


            var trusteePerson = new Trustee("trustee", "Andrew");
            var trustee = new User(trusteePerson, "Andrew");

            Users.AddAsync(trustee);

            SaveChanges();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasKey(order => order.Id)
            .HasName("PK_OrderId");

        modelBuilder.Entity<User>()
            .HasKey(d => d.Id)
            .HasName("PK_UserId");

        modelBuilder.Entity<DishMenu>()
    .HasKey(t => new { t.DishId, t.MenuId});

        modelBuilder.Entity<DishMenu>()
            .HasOne(dm => dm.dish)
            .WithMany(d => d.dishMenus)
            .HasForeignKey(dm => dm.DishId);

        modelBuilder.Entity<DishMenu>()
            .HasOne(dm => dm.menu)
            .WithMany(m => m.dishMenus)
            .HasForeignKey(dm => dm.MenuId);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        JObject o2;
        using (StreamReader file = File.OpenText("..\\webAplication.DAL\\Properties\\dbConnectionSettings.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            o2 = (JObject)JToken.ReadFrom(reader);
        }

        
        optionsBuilder.UseNpgsql($"Host={o2["Host"]};Port={o2["Port"]};Database={o2["Database"]};Username={o2["Username"]};Password={o2["Password"]}");
    }
}