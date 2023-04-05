using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using webAplication.Domain.Persons;

namespace webAplication.Service.Models
{
    public class AddExistingDishToMenuViewModel
    {
        [JsonProperty("MenuId")]
        [Required]
        public string menuId { get; set; }
        [JsonProperty("DishIds")]
        [Required]
        public string[] dishIds { get; set; }

        public static AddExistingDishToMenuViewModel? FromJson(string jsonObj)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<AddExistingDishToMenuViewModel>(jsonObj);

                if (obj.dishIds == null | obj.dishIds.Length == 0 | obj.menuId == null)
                    return null;

                return obj;
            }
            catch
            {
                return null;
            }
        }

    }
}
