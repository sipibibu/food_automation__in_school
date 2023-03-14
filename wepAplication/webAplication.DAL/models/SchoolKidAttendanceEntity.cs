using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class SchoolKidAttendanceEntity
    {
        [Key]
        public string schoolKidId;
        public SchoolKidAttendanceType Attendance;
        public SchoolKidAttendanceEntity() { }
    }
    public enum SchoolKidAttendanceType
    {
        Unknown,
        Missing,
        Present
    }
}
