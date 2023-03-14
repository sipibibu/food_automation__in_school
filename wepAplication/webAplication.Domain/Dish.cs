using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace wepAplication
{
    public class Dish : INstance
    {
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();
        public string? imageId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<DishMenuEntity> dishMenus = new List<DishMenuEntity>();
        public string imageFilePath;
        public double price { get; set; }//to decimal

        private Dish(DishEntity entity) 
        {
            id= entity.Id;
            imageId= entity.ImageId;
            title= entity.Title;
            description= entity.Description;
            price = entity.Price;
            imageFilePath= entity.ImageFilePath;
            dishMenus = entity.DishMenus;
        }

        public DishEntity ToEntity()
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
