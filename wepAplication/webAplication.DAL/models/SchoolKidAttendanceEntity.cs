using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class SchoolKidAttendanceEntity
    {
        [Key]
        public string SchoolKidId { get; set; }
        public SchoolKidAttendanceType Attendance { get; set; }
        public SchoolKidAttendanceEntity() { }
    }
}
