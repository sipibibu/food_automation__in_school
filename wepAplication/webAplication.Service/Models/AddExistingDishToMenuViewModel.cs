using System.ComponentModel.DataAnnotations;

namespace webAplication.Service.Models
{
    public class AddExistingDishToMenuViewModel
    {
        [Required]
        public string menuId { get; set; }
        [Required]
        public string[] dishIds { get; set; }
    }
}
