using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.DAL.models
{
    public class DishEntity
    {
        [Key]
        private string id;
        public string? imageId;

        public string title;
        public string description;

        public List<DishMenuEntity> dishMenus = new List<DishMenuEntity>();
        public string imageFilePath;

        public double price;//to decimal
    }
}
