using System.ComponentModel.DataAnnotations;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;
using wepAplication;

namespace webAplication.Domain
{
    public class Menu : IInstance<MenuEntity>
    {
        private Menu(){}
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();

        public String title { get; set; }
        public String description { get; set; }

        public TimeToService timeToService { get; set; }
        public  List<Dish> dishes { get; set; }
        private static Menu FromEntity(MenuEntity menuEntity)
        {
            var menu = new Menu();
            foreach (var dishMenu in menuEntity.DishMenus)
            {
                menu.dishes.Add(Dish.FromEntity(dishMenu.Dish));;
            }
        }
    }

    public enum TimeToService
    {
        breakfest,
        lunch,
        dinner
    }
}
