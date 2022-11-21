using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;

namespace webAplication.Service.Models
{
    public class MenuPutViewModel
    {
        [Required]
        public Menu Menu { get; set; }
        [Required]
        public string[] DishIds { get; set; }
    }
}
