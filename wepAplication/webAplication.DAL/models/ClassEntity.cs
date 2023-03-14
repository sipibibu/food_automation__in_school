using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class ClassEntity : IEntity
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string TeacherId { get; set; }
        public string[] SchoolKidIds { get; set; }
        public List<SchoolKidEntity> SchoolKids { get; set; }
    }
}
