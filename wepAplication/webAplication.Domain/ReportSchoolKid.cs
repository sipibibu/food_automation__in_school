using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class ReportSchoolKid
    {
        public string id { get; }
        public string name { get;}
        public Order? order { get;}

        public  ReportSchoolKid(SchoolKid kid,Order? order)
        {
            id = kid.Id;
            name = kid.name;
            this.order = order;
        }
    }
}