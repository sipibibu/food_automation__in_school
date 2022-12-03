using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class SchoolKidAttendance
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

    public enum SchoolKidAttendanceType
    {
        Uknown,
        Apsent,
        Missing
    }
}
