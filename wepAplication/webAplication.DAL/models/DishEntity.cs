using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class DishEntity : Entity
    {
        [Key]
        public string Id { get; set; }
        public string? ImageId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<DishMenuEntity> DishMenus = new List<DishMenuEntity>();
        public string ImageFilePath { get; set; }

        public double Price { get; set; }//to decimal
    }
}
