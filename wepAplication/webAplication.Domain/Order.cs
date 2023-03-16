using webAplication.DAL.models;
using webAplication.Domain.Interfaces;
using wepAplication;

namespace webAplication.Domain
{
    public class Order : ITransferredInstance<OrderEntity, Order>
    {
        private string _id;
        public string SchoolKidId { get; set; }
        public string MenuId { get; set; }
        public List<string> DishIds{ get; set; }
        public bool active;
        public long[] dates { get; set; }
        public virtual List<Dish> dishes { get; set; }
        public void Update(Order order)
        {
            this.SchoolKidId = order.SchoolKidId;
            this.MenuId = order.MenuId;
            this.DishIds = order.DishIds;
            this.active = order.active;
            this.dates = order.dates; 
        }

        public OrderEntity ToEntity()
        {
            throw new NotImplementedException();
        }

        public static Order ToInstance(OrderEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
