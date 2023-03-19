using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;
using webAplication.DAL.models.Persons;

namespace webAplication.DAL.models
{
    public class ClassEntity : Entity
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string TeacherId { get; set; }
        public string[] SchoolKidIds { get; set; }
        public List<SchoolKidEntity> SchoolKids { get; set; }
    }
}
