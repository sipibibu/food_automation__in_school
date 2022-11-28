using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Domain
{
    public class DishMenuOrder
    {
        public DishMenu dishMenu { get; set; }
        public Order order { get; set; }
    }
}
