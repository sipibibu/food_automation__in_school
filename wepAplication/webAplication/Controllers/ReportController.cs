/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Domain.Persons;
using Microsoft.AspNetCore.Hosting; // для IWebHostEnvironment
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.FileProviders.Physical;
using System.Net.Mime;

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
            db = context;
            attendances = db.Attendances;
            orders = db.Orders;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Report>> Get()
        {
            var atten = attendances.ToDictionary(x => x.schoolKidId);
            var order = orders.ToList();

            var report = new Report();
            foreach (var i in db.Person)   
            {

                *//*db.SchoolKids
                Так крче класс репорт должен быть имутабл(думаю логично что отчет изминяться не должен.)
                ну и логику вынести из контролера
                public class Report{
                    final List<T> data {get;}

                    
                    public Report(List<SchoolKid> schoolKids){ <- 
                        ну и тут твоя там логика где адд дата
                    }
                }
                 *//*
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
                    long dateNow = DateTimeOffset.Parse(DateTime.Now.ToShortDateString()).ToUnixTimeMilliseconds();
                    if (attendance.schoolKidAttendanceType == SchoolKidAttendanceType.Present && order.dates.Contains(dateNow))
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
                    StatusCode = Domain.StatusCode.OK,
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

        [HttpGet]
        [Route("[action]")]
        public async Task<FileStreamResult> GetExcel()
        {
            var orders = new Dictionary<TimeToService, List<Order>>();

            for (var i = 0; i < 3; i++)
            {
                orders[(TimeToService)i] = new List<Order>();
            }
            var ordersDb = db.Orders.ToList();
            foreach (var order in ordersDb)
            {
                var attendance = attendances.FirstOrDefault(x => x.schoolKidId == order.SchoolKidId);
                if (attendance.schoolKidAttendanceType == SchoolKidAttendanceType.Present)
                {
                    var menu = db.Menus.FirstOrDefault(x => x.Id == order.MenuId);
                    long dateNow = DateTimeOffset.Parse(DateTime.Now.ToShortDateString()).ToUnixTimeMilliseconds();
                    if (menu != null && order.dates.Contains(dateNow))
                    {
                        orders[menu.timeToService].Add(order);

                    }
                }
            }

            var schoolkids = db.SchoolKids.ToList();
            var dishes = db.Dishes.ToList();
            Report.CreateExcel(orders, schoolkids, dishes);

            FileStream fileStream = new FileStream("..\\webAplication\\Files\\report.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);


            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

        [HttpGet]
        [Route("[action]/{classId}")]
        public async Task<FileStreamResult> GetExcel(string classId)
        {

            var orders = new Dictionary<TimeToService, List<Order>>();
            var _class =db.Classes.FirstOrDefault(x=>x.Id==classId);

            for (var i = 0; i < 3; i++)
            {
                orders[(TimeToService)i] = new List<Order>();
            }
            var ordersDb = db.Orders.ToList();
            foreach (var order in ordersDb)
            {
                if (_class == null)
                {
                    continue;
                }
                
                var schoolkid = db.SchoolKids.FirstOrDefault(x => x.Id == order.Id);
                
                if(!_class.schoolKids.Contains(schoolkid))
                {
                    continue;
                }

                var attendance = attendances.FirstOrDefault(x => x.schoolKidId == order.SchoolKidId);
                if (attendance.schoolKidAttendanceType == SchoolKidAttendanceType.Present)
                {
                    var menu = db.Menus.FirstOrDefault(x => x.Id == order.MenuId);
                    long dateNow = DateTimeOffset.Parse(DateTime.Now.ToShortDateString()).ToUnixTimeMilliseconds();
                    if (menu != null && order.dates.Contains(dateNow))
                    {
                        orders[menu.timeToService].Add(order);

                    }
                }
            }

            var schoolkids = db.SchoolKids.ToList();
            var dishes = db.Dishes.ToList();
            Report.CreateExcel(orders, schoolkids, dishes);

            FileStream fileStream = new FileStream("..\\webAplication\\Files\\report.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);

            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }
    }
}
*/