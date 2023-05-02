using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using JsonKnownTypes;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    [JsonConverter(typeof(JsonKnownTypesConverter<Order>))]
    [JsonKnownType(typeof(Order), "Order")]
    public class Order : IInstance<Order.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Order>
        {
            [Key]
            public string Id { get; set; }
            public string SchoolKidId { get; set; }
            public string MenuId { get; set; }
            public Menu.Entity Menu { get; set; }
            public List<string> DishIds { get; set; }
            public List<Dish.Entity> Dishes { get; set; }
            public bool Active { get; set; }
            public long[] Dates { get; set; }
            public Entity() {}
            public Order ToInstance()
            {
                return new Order(this);
            }
            
        }
        [JsonProperty("Id")]
        private string Id { get; set; }
        [JsonProperty("SchoolKidId")]
        public string SchoolKidId { get; set; }
        [JsonProperty("MenuId")]
        public string MenuId { get; set; }
        
        [JsonProperty("Menu")]
        public Menu Menu { get; set; }
        
        [JsonProperty("Active")]
        private bool active;
        [JsonProperty("Dates")]
        public long[] dates { get; set; }
        [JsonProperty("Dishes")]
        public virtual List<Dish> dishes { get; set; } 
        [JsonProperty("DishesIds")]
        public List<string> DishesIds{ get; set; }

        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        private Order(Entity entity)
        {
            Id = entity.Id;
            dates = entity.Dates;
            active = entity.Active;
            MenuId = entity.MenuId;
            Menu = entity.Menu.ToInstance();
            DishesIds = entity.DishIds;
            dishes = entity.Dishes.Select(x => x.ToInstance()).ToList();
            SchoolKidId = entity.SchoolKidId;
        }
        public static Order? FromJsonPost(string jsonObj)
        {
            var obj=JsonConvert.DeserializeObject<Order>(jsonObj);
                obj.Id=Guid.NewGuid().ToString();

                if ( obj.SchoolKidId== null | obj.DishesIds==null | obj.dates==null) 
                    return null;
                return obj;
        }
        public static Order? FromJsonPut(string jsonObj)
        {
            var obj = JsonConvert.DeserializeObject<Order>(jsonObj);
                if (obj.Id==null | obj.SchoolKidId == null | obj.DishesIds == null | obj.dates == null | obj.dishes == null)
                    return null;
                return obj;
        }
        public void Update(Order order)
        {
            this.SchoolKidId = order.SchoolKidId;
            this.MenuId = order.MenuId;
            this.DishesIds = order.DishesIds;
            this.dishes = order.dishes;
            this.active = order.active;
            this.dates = order.dates; 
        }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = this.Id,
                Dates = dates,
                Active = active,
                MenuId = MenuId,
                Menu = this.Menu.ToEntity(),
                DishIds = DishesIds,
                Dishes = dishes == null ? null : dishes.Select(x => x.ToEntity()).ToList(),
                SchoolKidId = SchoolKidId,
            };
        }
    }
}
