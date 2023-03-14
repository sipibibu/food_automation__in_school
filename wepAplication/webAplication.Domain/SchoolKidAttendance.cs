using webAplication.Domain.Persons;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class SchoolKidAttendance : IInstance
    {
        public string schoolKidId { get; set; }

        SchoolKidAttendanceType Attendance = SchoolKidAttendanceType.Uknown;
        public SchoolKidAttendanceType schoolKidAttendanceType { get { return Attendance; } set { Attendance = value; } }
        
        public SchoolKidAttendance() { }
        public SchoolKidAttendance(SchoolKid kid)
        {
            schoolKidId = kid.Id;
        }
        public SchoolKidAttendance(string id,SchoolKidAttendanceType attendance)
        {
            schoolKidId = id;
            Attendance = attendance;
        }

    }
}
