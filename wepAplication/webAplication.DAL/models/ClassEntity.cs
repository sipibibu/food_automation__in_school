using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class ClassEntity
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string TeacherId { get; set; }
        public string[] SchoolKidIds { get; set; }
        public List<SchoolKidEntity> SchoolKids { get; set; }
    }
}
