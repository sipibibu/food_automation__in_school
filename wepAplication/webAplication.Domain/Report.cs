using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class Report
    {

        public List<ReportSchoolKid> data { get; set; }
        public Report() 
        {
            data= new List<ReportSchoolKid>();
        }

        public void AddData(SchoolKid schoolKid,SchoolKidAttendanceType attendance,Order? menu) 
        {
            if (attendance == SchoolKidAttendanceType.Apsent)
                data.Add(new ReportSchoolKid(schoolKid,menu));
        }
        
    }
}
