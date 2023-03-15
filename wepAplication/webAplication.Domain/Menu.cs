using webAplication.DAL.models;
using webAplication.Domain.Interfaces;
using wepAplication;

namespace webAplication.Domain
{
    public class Menu : ITransferredInstance<MenuEntity, Menu>
    {
        private Menu(){}
        public string Id { get { return _id; } set { } }
        private string _id = Guid.NewGuid().ToString();

        public string? title { get; set; }
        public string? description { get; set; }
        public  TimeToService timeToService { get; set; }
        private readonly HashSet<Dish> _dishes = new HashSet<Dish>();
        public static Menu FromEntity(MenuEntity menuEntity)
        {
            return new Menu(menuEntity);
        }
        public MenuEntity ToEntity()
        {
            return new MenuEntity()
            {
                Id = _id,
                Title = title,
                Description = description,
                TimeToService = timeToService,
                DishMenus = null, //write function that return DishMenus from _dishes 
            };
        }

        private Menu(MenuEntity entity)
        {
            _id = entity.Id;
            title = entity.Title;
            description = entity.Description;
            timeToService = entity.TimeToService;
            foreach (var dishMenu in entity.DishMenus)
            {
                _dishes.Add(Dish.FromEntity(dishMenu.Dish));;
            }
        }
    }
}
