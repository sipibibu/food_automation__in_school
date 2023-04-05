using System.ComponentModel.DataAnnotations;
using JsonKnownTypes;
using Newtonsoft.Json;
using OfficeOpenXml.DataValidation;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    [JsonConverter(typeof(JsonKnownTypesConverter<Menu>))]
    [JsonKnownType(typeof(Menu), "Menu")]
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
        /*
        {"$type":"Menu","Title":"asdsa","Description":"asdfasdf","TimeToService":1,"Dishes":[]}
         */
        [JsonProperty("Id")]
        private string _id;
        [JsonProperty("Title")]
        private string _title;
        [JsonProperty("Description")]
        private string _description;
        [JsonProperty("TimeToService")]
        private TimeToService _timeToService;
        [JsonProperty("Dishes")]
        private List<string>? _dishesIds;
        private HashSet<Dish> _dishes;
        private Menu() { }
        private Menu(Entity entity)
        {
            _id = entity.Id;
            _title = entity.Title;
            _description = entity.Description;
            _timeToService = entity.TimeToService;
            if (entity.Dishes == null)
                entity.Dishes = new List<Dish.Entity>();
            foreach (var dish in entity.Dishes)
            {
                _dishes.Add(dish.ToInstance());
            }
        }
        public void addDishes(IEnumerable<Dish> dishes)
        {
            this._dishes.Concat(dishes);
        }
        public void addDishes(IEnumerable<Dish.Entity> dishes)
        {
            var res= new HashSet<Dish>();
            foreach (var dish in dishes)
            {
                res.Add(dish.ToInstance());
            }
            this._dishes.Concat(res);
        }
        public static Menu ToInstance(Entity? menuEntity)
        {
            if (menuEntity==null)
                return null;
            return new Menu(menuEntity);
        }
        public static Menu? FromJsonPost(string jsonObj)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<Menu>(jsonObj);
                obj._id = Guid.NewGuid().ToString();
                obj._dishes = new HashSet<Dish>();
                obj._dishesIds = new List<string>();
                return obj;
            }
            catch
            {
                return null;
            }
        }
        public static Menu? FromJsonPut(string jsonObj)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<Menu>(jsonObj);

                return obj;
            }
            catch
            {
                return null;
            }
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
