using JsonKnownTypes;
using Newtonsoft.Json;
using webAplication.Domain.Persons;

namespace webAplication.Domain;

public class BuffetMenu : Menu
{
    public class Entity : Menu.Entity
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? DishesIds { get; set; }
        public HashSet<Dish.Entity>? Dishes { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public List<DishMenu.Entity> DishMenus { get; set; }
        public TimeToService TimeToService { get; set; }
        public Entity() : base()
        {
        }
        public BuffetMenu ToInstance()
        {
            return new BuffetMenu(this);
        }
        
    }

    public string Json()
    {
        return JsonConvert.SerializeObject(this);
    }
    public BuffetMenu() : base()
    {
        
    }
    public BuffetMenu(Entity entity) : base(entity)
    {
        // _id = entity.Id;
        // _description = entity.Description;
        // _dishes = entity.Dishes.Select(x => x.ToInstance()).ToHashSet();
        // _title = entity.Title;
        // _timeToService = entity.TimeToService;
        // _dishesIds = entity,
    }
    public Entity ToEntity()
    {
        return new BuffetMenu.Entity()
        {
            Id = _id,
            Title = _title,
            Description = _description,
            TimeToService = _timeToService,
            DishesIds=_dishesIds,
            Dishes = _dishes.Select(x => x.ToEntity()).ToHashSet()
        };
    }
}