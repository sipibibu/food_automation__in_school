namespace webAplication.Domain
{
    public class Order
    {
        public string Id { get { return id; } set { } }
        public string SchoolKidId { get; set; }
        private string id = Guid.NewGuid().ToString();
        public string MenuId { get; set; }
        public List<string> DishIds{ get; set; }
        public bool active;

        public void Update(Order order)
        {
            this.SchoolKidId = order.SchoolKidId;
            this.MenuId = order.MenuId;
            this.DishIds = order.DishIds;
            this.active = order.active;
        }
    }
}
