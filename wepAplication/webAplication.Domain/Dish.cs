using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonKnownTypes;
using Newtonsoft.Json;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonKnownTypesConverter<Dish>))]
    [JsonKnownType(typeof(Dish), "Dish")]

    public class Dish : IInstance<Dish.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Dish>
        {
            [Key]
            public string Id { get; set; }
            public string? ImageId { get; set; }

            public string Title { get; set; }
            public string Description { get; set; }
            
            [Newtonsoft.Json.JsonIgnore]
            public IEnumerable<Menu.Entity> Menus = new List<Menu.Entity>();
            [Newtonsoft.Json.JsonIgnore]
            public IEnumerable<DishMenu.Entity> DishMenus = new List<DishMenu.Entity>();

            public double Price { get; set; }//to decimal
            public Dish ToInstance()
            {
                return new Dish(this);
            }
        }
        [JsonProperty("Id")]
        private string _id;
        [JsonProperty("Title")]
        private string _title;
        [JsonProperty("Price")]
        private double _price;
        [JsonProperty("ImageId")]
        private string? _imageId;
        [JsonProperty("Description")]
        private string _description;
        [Newtonsoft.Json.JsonIgnore]
        private HashSet<Menu> _menus = new ();

        public Dish(string imageId,string title,string description,double price)
        {   
            _id = Guid.NewGuid().ToString();
            _imageId = imageId;
            _title = title;
            _description= description;
            _price = price;
        }
        
        public static Dish? FromJsonPost(string jsonObj)
        {
            var obj=JsonConvert.DeserializeObject<Dish>(jsonObj);
            obj._id = Guid.NewGuid().ToString();

            if ( obj._title ==null ) 
                return null;
            return obj;
        }
        public static Dish? FromJsonPut(string jsonObj)
        {
            var obj = JsonConvert.DeserializeObject<Dish>(jsonObj);
            if (obj._id == null)
                return null;
            return obj;
        }
        
        private Dish(Entity entity) 
        {
            _id= entity.Id;
            _imageId= entity.ImageId;
            _title= entity.Title;
            _description= entity.Description;
            _price = entity.Price;
        }
        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = _id,
                ImageId = _imageId,
                Title = _title,
                Description = _description,
                Price = _price,
            };
        }
    }
}
