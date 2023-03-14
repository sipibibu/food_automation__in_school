using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class OrderEntity : IEntity
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
