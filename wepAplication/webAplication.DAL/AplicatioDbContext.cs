﻿using Microsoft.EntityFrameworkCore;
using webAplication.Models;
using wepAplication;

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
        optionsBuilder.UseNpgsql("Host=192.168.1.5;Port=5432;Database=usersdb;Username=postgres;Password=mysecretpassword");
    }
}