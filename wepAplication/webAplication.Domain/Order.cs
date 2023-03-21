using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class Order : IInstance<Order.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<IInstance<Entity>>
        {
            [Key]
            public string Id { get; set; }
            public string SchoolKidId { get; set; }
            public string MenuId { get; set; }
            public List<string> DishIds { get; set; }
            public bool Active { get; set; }
            public long[] Dates { get; set; }
            public Entity() {}
            public IInstance<Entity> ToInstance()
            {
                return new Order(this);
            }
            
        }
        public string Id { get { return id; } set { } }
        public string SchoolKidId { get; set; }
        private string id = Guid.NewGuid().ToString();

        public string MenuId { get; set; }
        public List<string> DishIds{ get; set; }
        public bool active;

        public long[] dates { get; set; }
        public virtual List<Dish> dishes { get; set; } 

        private Order()
        {
            throw new NotImplementedException();
        }

        private Order(Entity entity)
        {
            id = entity.Id;
            dates = entity.Dates;
            active = entity.Active;
            MenuId = entity.MenuId;
            DishIds = entity.DishIds;
            SchoolKidId = entity.SchoolKidId;
        }
        public void Update(Order order)
        {
            this.SchoolKidId = order.SchoolKidId;
            this.MenuId = order.MenuId;
            this.DishIds = order.DishIds;
            this.active = order.active;
            this.dates = order.dates; 
        }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = id,
                Dates = dates,
                Active = active,
                MenuId = MenuId,
                DishIds = DishIds,
                SchoolKidId = SchoolKidId,
            };
        }
    }
}
