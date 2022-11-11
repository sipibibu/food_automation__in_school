using System.ComponentModel.DataAnnotations;
using webAplication.Domain;

namespace wepAplication
{
    public class Dish
    {
        [Key]
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();

        public string title { get; set; }
        public string description { get; set; }

        public List<DishMenu> dishMenus = new List<DishMenu>();
        public string imageFilePath;

        public double price { get; set; }//to decimal
    }
}
