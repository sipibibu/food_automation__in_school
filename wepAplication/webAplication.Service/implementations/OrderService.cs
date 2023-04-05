using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.implementations
{
    public class OrderService : IOrderService
    {
        private AplicationDbContext db;
        private readonly ILogger<OrderService> _logger;


        public OrderService(ILogger<OrderService> logger, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
        }
        public async Task<BaseResponse<IActionResult>> Post(string jsonObj)
        {
            try
            {
                var obj = Order.FromJsonPost(jsonObj);
                if (obj == null)
                    return new BaseResponse<IActionResult>
                    {
                        Description = "Wrong json format.",
                        StatusCode = StatusCode.BAD
                    };
                db.Orders.Add(obj.ToEntity());
                db.SaveChanges();
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[DeleteMenu]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IActionResult>> Put(string jsonObj)
        {
            try
            {
                var order = Order.FromJsonPut(jsonObj).ToEntity();
                if (order == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "Wrong JSON format"
                    };
                }
                if (await db.Orders.FirstOrDefaultAsync(x => x.Id == order.Id) == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no order with that id:{order.Id}"
                    };
                }

                var schoolkid = await db.SchoolKids.FirstOrDefaultAsync(x => x.Id == order.SchoolKidId);
                if (schoolkid == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "there is no schoolkid with that id"
                    };
                }

                db.Orders.Update(order);
                db.SaveChanges();
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[ChangeOrder]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }

        }

        public async Task<BaseResponse<IEnumerable<Order.Entity>>> Get()
        {
            try
            {
                var orders = await db.Orders.ToListAsync();
                return new BaseResponse<IEnumerable<Order.Entity>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = orders,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<IEnumerable<Order.Entity>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Order.Entity>> Get(string id)
        {
            try
            {
                var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                {
                    return new BaseResponse<Order.Entity>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "Net takogo ordera"
                    };
                }
                return new BaseResponse<Order.Entity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = order,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<Order.Entity>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Order.Entity>>> GetSchoolKidsOrders(string schoolKidId)
        {
            try
            {
                var schoolKid = await db.SchoolKids.FirstOrDefaultAsync(sk => sk.Id == schoolKidId);
                if (schoolKid == null)
                {
                    return new BaseResponse<IEnumerable<Order.Entity>>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no schoolKid with that id: {schoolKidId}"
                    };
                }

                var orders = db.Orders.Where(order => order.SchoolKidId == schoolKidId).ToList();
                return new BaseResponse<IEnumerable<Order.Entity>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = orders,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<IEnumerable<Order.Entity>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IActionResult>> Delete(string id)
        {
            try
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "Net takogo ordera"
                    };
                }
                db.Orders.Remove(order);
                db.SaveChanges();
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
    }
}
