using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Service.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public string menuId { get; set; }
        [Required]
        public string[] dishIds { get; set; }
        [Required]
        public string SchoolKidId { get; set; }
        [Required]
        public long[] dates { get; set; }
    }
}
