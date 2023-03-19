using webAplication.Domain.Interfaces;
using wepAplication;

namespace webAplication.Domain
{
    public class Order : IInstance<OrderEntity>
    {
        public string Id { get { return id; } set { } }
        public string SchoolKidId { get; set; }
        private string id = Guid.NewGuid().ToString();

        public string MenuId { get; set; }
        public List<string> DishIds{ get; set; }
        public bool active;

        public long[] dates { get; set; }
        public virtual List<Dish> dishes { get; set; } 

        public Order()
        {
            dishes = new List<Dish>();
        }
        public void Update(Order order)
        {
            this.SchoolKidId = order.SchoolKidId;
            this.MenuId = order.MenuId;
            this.DishIds = order.DishIds;
            this.active = order.active;
            this.dates = order.dates; 
        }
    }
}
