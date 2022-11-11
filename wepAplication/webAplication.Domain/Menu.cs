using System.ComponentModel.DataAnnotations;
using wepAplication;

namespace webAplication.Domain
{
    public class Menu
    {
        [Key]
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();

        public String title { get; set; }
        public String description { get; set; }

        public List<DishMenu> dishMenus = new List<DishMenu>();
        public TimeToService timeToService { get; set; }

        public virtual IEnumerable<Dish> dishes { get; set; }
    }

    public enum TimeToService
    {
        breakfest,
        lunch,
        dinner
    }
}
