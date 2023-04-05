using Newtonsoft.Json;
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
        [JsonProperty("Id")]
        private string Id { get; set; }
        [JsonProperty("SchoolKidId")]
        private string SchoolKidId { get; set; }
        [JsonProperty("MenuId")]
        private string MenuId { get; set; }
        [JsonProperty("DishIds")]
        private List<string> DishIds{ get; set; }
        [JsonProperty("Active")]
        private bool active;
        [JsonProperty("Dates")]
        private long[] dates { get; set; }
        [JsonProperty("Dishes")]
        public virtual List<Dish> dishes { get; set; } 

        private Order()
        {
            throw new NotImplementedException();
        }

        private Order(Entity entity)
        {
            Id = entity.Id;
            dates = entity.Dates;
            active = entity.Active;
            MenuId = entity.MenuId;
            DishIds = entity.DishIds;
            SchoolKidId = entity.SchoolKidId;
        }
        public static Order? FromJsonPost(string jsonObj)
        {
            try
            {
                var obj=JsonConvert.DeserializeObject<Order>(jsonObj);
                obj.Id=Guid.NewGuid().ToString();

                if ( obj.SchoolKidId== null | obj.DishIds==null | obj.dates==null | obj.dishes==null) 
                    return null;
                return obj;
            }
            catch
            {
                return null;
            }
        }
        public static Order? FromJsonPut(string jsonObj)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<Order>(jsonObj);
                if (obj.Id==null | obj.SchoolKidId == null | obj.DishIds == null | obj.dates == null | obj.dishes == null)
                    return null;
                return obj;
            }
            catch
            {
                return null;
            }
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
                Id = this.Id,
                Dates = dates,
                Active = active,
                MenuId = MenuId,
                DishIds = DishIds,
                SchoolKidId = SchoolKidId,
            };
        }
    }
}
