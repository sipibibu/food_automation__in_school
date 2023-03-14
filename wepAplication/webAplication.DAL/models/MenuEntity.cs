using System.ComponentModel.DataAnnotations;
using wepAplication;

namespace webAplication.DAL.models
{
    public class MenuEntity
    {
        [Key]
        public string id;
        public string? title;
        public string? description;
        public List<DishMenuEntity> dishMenus = new List<DishMenuEntity>();
        public TimeToService timeToService { get; set; }
    }
    public enum TimeToService
    {
        Breakfast,
        Lunch,
        Dinner
    }
}
