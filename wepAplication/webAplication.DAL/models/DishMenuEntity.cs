using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class DishMenuEntity
    {
        [Key]
        public string DishId { get; set; }
        public string MenuId { get; set; }
        public MenuEntity Menu { get; set; }
        public DishEntity Dish { get; set; }
    }
}