using webAplication.Domain.Persons;

namespace webAplication.Domain;

public class DishMenu
{
    public class Entity
    {
        public string Id { get; set; }
        public Dish.Entity Dish { get; set; }
        public Menu.Entity Menu { get; set; }
        public string DishId { get; set; }
        public string MenuId { get; set; }
        public long? ServiceDate { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Entity(Menu.Entity menu, Dish.Entity dish, long? date)
        {
            Id = Guid.NewGuid().ToString();
            Dish = dish;
            DishId = dish.Id;
            Menu = menu;
            MenuId = menu.Id;

            ServiceDate = date;
        }
    }
}   