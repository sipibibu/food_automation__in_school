using webAplication.DAL.models;
using webAplication.Domain.Interfaces;


namespace wepAplication
{
    public class Dish : ITransferredInstance<DishEntity, Dish>
    {
        private string id;
        public string? imageId { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public double price { get; set; }//to decimal

/*        public List<DishMenuEntity> dishMenus = new List<DishMenuEntity>();
*/ 
        public Dish(string imageId,string title,string description,double price)
        {   
            this.id = Guid.NewGuid().ToString();
            this.imageId = imageId;
            this.title = title;
            Description= description;
            this.price = price;
        }
        private Dish(DishEntity entity) 
        {
            id= entity.Id;
            imageId= entity.ImageId;
            title= entity.Title;
            Description= entity.Description;
            price = entity.Price;
        }
        public static Dish FromEntity(DishEntity entity)
        {
            return new Dish(entity);
        }
        public DishEntity ToEntity()
        {
            return new DishEntity()
            {
                Id = id,
                ImageId = imageId,
                Title = title,
                Description = Description,
                Price = price,
            };
        }
    }
}
