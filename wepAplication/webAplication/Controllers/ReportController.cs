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
            var order = orders.ToList();

            var report = new Report();
            foreach(var i in db.Person)
            {
                if (i.role == "SchoolKid")
                {
                    var kidOrders = order.FindAll(x => x.SchoolKidId == i.Id);
                    if (kidOrders.Count != 0) 
                    { 
                        report.AddData((SchoolKid)i, atten[i.Id].schoolKidAttendanceType, kidOrders);
                    }
                    else
                        report.AddData((SchoolKid)i, atten[i.Id].schoolKidAttendanceType, null);
                }
            }
            return report;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Order>>> GetByClass(string classId)
        {
            try
            {
                var _class = db.Classes.FirstOrDefault(c => c.Id == classId);
                if (_class == null)
                    return new BaseResponse<IEnumerable<Order>>()
                    {
                        StatusCode = Domain.StatusCode.BAD,
                        Description = $"there is no class with that id: {classId}"
                    };

                var orders = new List<Order>();
                foreach (var order in db.Orders.ToList())
                {
                    var attendance = db.Attendances.FirstOrDefault(x => x.schoolKidId == order.SchoolKidId);
                    if (attendance.schoolKidAttendanceType == SchoolKidAttendanceType.Present)
                    {
                        foreach (var dishId in order.DishIds)
                        {
                            var dish = db.Dishes.FirstOrDefault(x => x.Id == dishId);
                            if (dish == null)
                                continue;
                            order.dishes.Add(dish);
                        }
                        orders.Add(order);
                    }

                }

                return new BaseResponse<IEnumerable<Order>>()
                {
                    StatusCode=Domain.StatusCode.OK,
                    Data = orders,
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<IEnumerable<Order>>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }
    }
}
