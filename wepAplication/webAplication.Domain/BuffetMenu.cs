using JsonKnownTypes;
using Newtonsoft.Json;
using webAplication.Domain.Persons;

namespace webAplication.Domain;

public class BuffetMenu : Menu
{
    public class Entity : Menu.Entity
    {
        public Entity() : base()
        {
        }
        public new Menu ToInstance()
        {
            return new BuffetMenu(this);
        }
        
    }

    public BuffetMenu() : base()
    {
        
    }
    public BuffetMenu(Entity entity) : base(entity)
    {
        
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