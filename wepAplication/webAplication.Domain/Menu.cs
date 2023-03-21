using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Menu : IInstance<Menu.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Menu>
        {
            [Key]
            public string Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public List<Dish.Entity> Dishes { get; set; }
            public TimeToService TimeToService { get; set; }
            public Menu ToInstance()
            {
                return new Menu(this);
            }
        }
        public enum TimeToService
        {
            Breakfast,
            Lunch,
            Dinner
        }
        private string _id;
        private string _title;
        private string _description;
        private TimeToService _timeToService;
        private readonly HashSet<Dish> _dishes = new HashSet<Dish>();
        private Menu() { throw new Exception(); }
        private Menu(Entity entity)
        {
            _id = entity.Id;
            _title = entity.Title;
            _description = entity.Description;
            _timeToService = entity.TimeToService;
            foreach (var dish in entity.Dishes)
            {
                _dishes.Add(dish.ToInstance());
            }
        }
        public static Menu ToInstance(Entity menuEntity)
        {
            return new Menu(menuEntity);
        }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = _id,
                Title = _title,
                Description = _description,
                TimeToService = _timeToService,
                Dishes = _dishes.Select(x => x.ToEntity()).ToList()
            };
        }
    }
}
