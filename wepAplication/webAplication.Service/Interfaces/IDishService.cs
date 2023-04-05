using webAplication.Domain;

namespace webAplication.Service.Interfaces;

public interface IDishService
{
    Dish? GetDish(string dishId);
    IEnumerable<Dish> GetDishes();
    Dish CreateDish(Dish dish);
    Dish UpdateDish(Dish dish);
    Dish DeleteDish(string dishId);
}