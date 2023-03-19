using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace wepAplication
{
    public class Dish : ITransferredInstance<DishEntity, Dish>
    {
        private string _id;
        private string _title;
        private double _price;
        private string? _imageId;
        private string _description;

        public Dish(string imageId,string title,string description,double price)
        {   
            _id = Guid.NewGuid().ToString();
            _imageId = imageId;
            _title = title;
            _description= description;
            _price = price;
        }
        private Dish(DishEntity entity) 
        {
            _id= entity.Id;
            _imageId= entity.ImageId;
            _title= entity.Title;
            _description= entity.Description;
            _price = entity.Price;
        }
        public static Dish ToInstance(DishEntity entity)
        {
            return new Dish(entity);
        }
        public DishEntity ToEntity()
        {
            return new DishEntity()
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
