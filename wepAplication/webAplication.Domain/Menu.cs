using System.ComponentModel.DataAnnotations;
using JsonKnownTypes;
using Microsoft.EntityFrameworkCore;
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
            public List<string>? DishesIds { get; set; }
            public HashSet<Dish.Entity>? Dishes { get; set; }
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
        {"$type":"Menu","Id":"4704a9e5-6c89-465d-b126-33f54366556a","Title":"oko","Description":"asdfasdf","TimeToService":1,"Dishes":["1"]}

         */
        [JsonProperty("Id")]
        private string _id;
        [JsonProperty("Title")]
        private string _title;
        [JsonProperty("Description")]
        private string _description;
        [JsonProperty("TimeToService")]
        private TimeToService _timeToService;
        [JsonProperty("DishesId")]
        private List<string>? _dishesIds=new List<string>();
        private HashSet<Dish>? _dishes=new HashSet<Dish>();
        private Menu() { }
        private Menu(Entity entity)
        {
            _id = entity.Id;
            _title = entity.Title;
            _description = entity.Description;
            _timeToService = entity.TimeToService;
            _dishesIds = entity.DishesIds;
            if (_dishesIds == null)
            {
                _dishesIds = new List<string>();
            }
            _dishes=new HashSet<Dish>();
   
        }
        public IEnumerable<string> GetDishesIds()
        {
            return _dishesIds;
        }
        public void ChangeDihseIds(List<string> dishesIds)
        {
            _dishesIds = dishesIds;
        }
        public void addDishes(IEnumerable<Dish> dishes)
        {
            this._dishes.Concat(dishes);
        }
        public void addDishes(IEnumerable<Dish.Entity> dishes)
        {
            foreach (var dish in dishes)
            {
                this._dishes.Add(dish.ToInstance());
                this._dishesIds.Add(dish.Id);
            }
        }
        public static Menu ToInstance(Entity? menuEntity)
        {
            if (menuEntity==null)
                return null;
            return new Menu(menuEntity);
        }
        public static Menu FromJsonPost(string jsonObj)
        {
                var obj = JsonConvert.DeserializeObject<Menu>(jsonObj);
                obj._id = Guid.NewGuid().ToString();
                return obj;
        }
        public void LoadDishes(DbSet<Dish.Entity> db)
        {
            foreach(var dishId in this._dishesIds)
            {
                var d = db.FirstOrDefault(x=>x.Id==dishId);
                if (d != null)
                    this._dishes.Add(d.ToInstance());
            }
        }
        public static Menu FromJsonPut(string jsonObj)
        {
                var obj = JsonConvert.DeserializeObject<Menu>(jsonObj);

                return obj;
        }
        public Entity ToEntity()
        {
            return new Entity()
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
}
