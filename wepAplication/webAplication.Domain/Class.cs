using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class Class
    {
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();
        public string title { get; set; }
        public string teacherId { get; set; }    
        public string[] schoolKidIds { get; set; }
        public virtual List<SchoolKid> schoolKids { get; set; }

        public Class()
        {
            schoolKids = new List<SchoolKid>();
        }
        public void Update(Class _class)
        {
            title = _class.title;
            teacherId = _class.teacherId;
            schoolKidIds = _class.schoolKidIds;
        }
    }
}
