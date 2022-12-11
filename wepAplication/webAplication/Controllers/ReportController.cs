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

        [HttpGet]
        [Route("[action]{classId}")]
        public async Task<BaseResponse<Report>> Get(string classId)
        {
            try
            {
                var _class = db.Classes.FirstOrDefault(c => c.Id == classId);
                if (_class == null)
                    return new BaseResponse<Report>()
                    {
                        StatusCode = Domain.StatusCode.BAD,
                        Description = $"there is no class with that id: {classId}"
                    };

                var report = new Report();
                foreach (var schoolkidId in _class.schoolKidIds)
                {
                    var order = db.Orders.FirstOrDefault(x => x.SchoolKidId == schoolkidId);
                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolkidId);
                    var attendance = db.Attendances.FirstOrDefault(x => x.schoolKidId == schoolkidId);
                    report.AddData((SchoolKid)schoolKid, attendance.schoolKidAttendanceType, order);
                }
                return new BaseResponse<Report>()
                {
                    StatusCode=Domain.StatusCode.OK,
                    Data = report,
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<Report>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }
    }
}
