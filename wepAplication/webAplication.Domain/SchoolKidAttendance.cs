using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Domain
{
    public class SchoolKidAttendance
    {
        public string schoolKidId { get; set; }
        public SchoolKidAttendanceType schoolKidAttendanceType { get; set; }
    }

    public enum SchoolKidAttendanceType
    {
        Uknown,
        Apsent,
        Missing
    }
}
