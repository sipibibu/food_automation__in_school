using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class MenuEntity : IEntity
    {
        [Key]
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<DishMenuEntity> DishMenus { get; set; }
        public TimeToService TimeToService { get; set; }
    }
    public enum TimeToService
    {
        Breakfast,
        Lunch,
        Dinner
    }
}
