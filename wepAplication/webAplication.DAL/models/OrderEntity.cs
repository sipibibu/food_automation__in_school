using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class OrderEntity
    {
        [Key]
        public string Id { get; set; }
        public string MenuId { get; set; }
        public List<string> DishIds { get; set; }
        public bool Active { get; set; }
        public long[] Dates { get; set; }
        public OrderEntity() {}
    }
}
