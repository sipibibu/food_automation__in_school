using webAplication.DAL.models;
using webAplication.Domain.Interfaces;
using wepAplication;

namespace webAplication.Domain
{
    public class Menu : ITransferredInstance<MenuEntity, Menu>
    {
        private string _id;
        private string _title;
        private string _description;
        private TimeToService _timeToService;
        private readonly HashSet<Dish> _dishes = new HashSet<Dish>();
        private Menu() { throw new Exception(); }
        private Menu(MenuEntity entity)
        {
            _id = entity.Id;
            _title = entity.Title;
            _description = entity.Description;
            _timeToService = entity.TimeToService;
            foreach (var dishMenu in entity.DishMenus)
            {
                _dishes.Add(Dish.ToInstance(dishMenu.Dish));;
            }
        }
        public static Menu ToInstance(MenuEntity menuEntity)
        {
            return new Menu(menuEntity);
        }

        public MenuEntity ToEntity()
        {
            return new MenuEntity()
            {
                Id = _id,
                Title = _title,
                Description = _description,
                TimeToService = _timeToService,
                DishMenus = _dishes.Select(x => new DishMenuEntity()
                {
                    Dish = x.ToEntity(),
                    DishId = x.ToEntity().Id, //code repeat
                    MenuId = _id
                }).ToList()
            };
        }
    }
}
