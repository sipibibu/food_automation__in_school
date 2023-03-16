using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class DishMenuEntity : Entity
    {
        [Key]
        public string DishId { get; set; }
        public string MenuId { get; set; }
        public MenuEntity Menu { get; set; }
        public DishEntity Dish { get; set; }
    }
}