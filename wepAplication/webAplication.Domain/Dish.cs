using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Dish : IInstance<Dish.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Dish>
        {
            [Key]
            public string Id { get; set; }
            public string? ImageId { get; set; }

            public string Title { get; set; }
            public string Description { get; set; }

            public List<Menu.Entity> Menus = new ();
            public string ImageFilePath { get; set; }

            public double Price { get; set; }//to decimal
            public Dish ToInstance()
            {
                throw new NotImplementedException();
            }
        }
        private string _id;
        private string _title;
        private double _price;
        private string? _imageId;
        private string _description;
        private HashSet<Menu> _menus = new ();

        public Dish(string imageId,string title,string description,double price)
        {   
            _id = Guid.NewGuid().ToString();
            _imageId = imageId;
            _title = title;
            _description= description;
            _price = price;
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
