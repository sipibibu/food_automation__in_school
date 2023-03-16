using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class SchoolKidAttendanceEntity : Entity
    {
        [Key]
        public string SchoolKidId { get; set; }
        public SchoolKidAttendanceType Attendance { get; set; }
        public SchoolKidAttendanceEntity() { }
    }
}
