using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class ClassEntity
    {
        [Key]
        private string id;
        public string title;
        public string teacherId;
        public string[] schoolKidIds;
        public List<SchoolKidEntity> schoolKids;
    }
}
