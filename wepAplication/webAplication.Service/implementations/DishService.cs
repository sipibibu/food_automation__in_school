using Microsoft.Extensions.Logging;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Service.implementations;

public class DishService : IDishService
{
    private AplicationDbContext db;

    private readonly ILogger<DishService> _logger;

    public DishService(ILogger<DishService> logger, AplicationDbContext context)
    {
        db = context;
        _logger = logger;
    }

    public Dish.Entity GetDish(string dishId)
    {
        return db.Dishes.FirstOrDefault(x => x.Id == dishId);
    }

    public IEnumerable<Dish.Entity> GetDishes()
    {
        return db.Dishes;
    }

    public Dish CreateDish(Dish dish)
    {
        db.Dishes.Add(dish.ToEntity());
        db.SaveChanges();
        return dish;
    }

    public Dish UpdateDish(Dish dish)
    {
        db.ChangeTracker.Clear();
        db.Dishes.Update(dish.ToEntity());
        db.SaveChanges();
        return dish;
    }

    public Dish DeleteDish(string dishId)
    {
        var dish = db.Dishes.FirstOrDefault(x => x.Id == dishId);
        db.Dishes.Remove(dish);
        db.SaveChanges();
        return dish.ToInstance();
    }
}