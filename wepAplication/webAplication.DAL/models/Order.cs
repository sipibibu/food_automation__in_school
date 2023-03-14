using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class Order
    {
        [Key]
        public string id;
        public string menuId;
        public List<string> dishIds;
        public bool active;
        public long[] dates;
        public Order() {}
    }
}
