using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Domain.Persons;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        AplicationDbContext db;
        DbSet<SchoolKidAttendance> attendances;
        DbSet<Order> orders;


        public ReportController(AplicationDbContext context)
        {
            db=context;
            attendances= db.Attendances;
            orders= db.Orders;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Report>> Get()
        {
            var atten = attendances.ToDictionary(x=>x.schoolKidId);
            var order = orders.ToDictionary(x => x.SchoolKidId);

            var report = new Report();
            foreach(var i in db.Person)
            {
                if (i.role == "SchoolKid")
                {
                    if(order.Keys.Contains(i.Id))
                    { 
                       report.AddData((SchoolKid)i, atten[i.Id].schoolKidAttendanceType, order[i.Id]);
                    }
                    else
                    {
                        report.AddData((SchoolKid)i, atten[i.Id].schoolKidAttendanceType, null);
                    }
                }
            }
            return report;
        }
    }
}
