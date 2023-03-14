using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wepAplication;

namespace webAplication.Domain
{
    public class DishMenu
    {
        public string DishId { get; set; }
        public Dish dish { get; set; }

        public string MenuId { get; set; }
        public Menu menu { get; set; }
    }
}
