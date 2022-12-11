using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class ReportSchoolKid
    {
        public string id { get; }
        public string name { get;}
        public List<Order> orders { get;}

        public  ReportSchoolKid(SchoolKid kid,List<Order>? order)
        {
            id = kid.Id;
            name = kid.name;
            if (order != null)
            {
                this.orders = new List<Order>();
                foreach (var i in order) {
                    this.orders.Add(i);
                }
            }
        }
    }
}