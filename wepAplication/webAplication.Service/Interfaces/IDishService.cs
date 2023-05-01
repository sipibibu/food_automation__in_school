using webAplication.Domain;

namespace webAplication.Service.Interfaces;

public interface IDishService
{
    Dish.Entity GetDish(string dishId);
    IEnumerable<Dish.Entity> GetDishes();
    Dish CreateDish(Dish dish);
    Dish UpdateDish(Dish dish);
    Dish DeleteDish(string dishId);
}