using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class DishMenuEntity
    {
        [Key]
        public string dishId;
        public string menuId;
        public MenuEntity Menu;
        public DishEntity dish;
    }
}