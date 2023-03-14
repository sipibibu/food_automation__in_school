using System.ComponentModel.DataAnnotations;
using webAplication.Domain;
using webAplication.DAL.models;
namespace wepAplication
{
    public class Dish
    {
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();
        public string? imageId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<DishMenu> dishMenus = new List<DishMenu>();
        public string imageFilePath;
        public double price { get; set; }//to decimal

        public Dish(DishEntity entity) 
        {
            id= entity.Id;
            imageId= entity.ImageId;
            title= entity.Title;
            description= entity.Description;
            price = entity.Price;
            imageFilePath= entity.ImageFilePath;
            dishMenus = entity.DishMenus;
        }

        public DishEntity toEntity()
        {
            return new DishEntity()
            {
                Id = id,
                ImageId = imageId,
                Title = title,
                Description = description,
                Price = price,
                ImageFilePath = imageFilePath,
                DishMenus = dishMenus
            };
        }
    }
}
