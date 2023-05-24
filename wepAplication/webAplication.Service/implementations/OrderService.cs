using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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
        private IDishService _dishService;
        private IMenuService _menuService;


        public OrderService(ILogger<OrderService> logger, IDishService dishService, IMenuService menuService, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
            _dishService = dishService;
            _menuService = menuService;
        }
        public Order Post(Order order)
        {
            order.dishes = order.DishesIds.Select(x => _dishService.GetDish(x).ToInstance()).ToList();
            order.Menu = _menuService.Get(order.MenuId).ToInstance();
            var orderEntity = order.ToEntity();
            orderEntity.Menu = _menuService.Get(order.MenuId);
            orderEntity.Dishes = _dishService
                .GetDishes()
                .Where(x => orderEntity.Dishes
                    .Select(j => j.Id)
                    .Contains(x.Id))
                .ToList();
            db.Orders.Add(orderEntity);
            db.SaveChanges();
            return order;
        }

        public IEnumerable<Order> OrderMenu(string schoolKidId, string menuId, int duration)
        {
            var index = (int)DateTime.Today.DayOfWeek - 1;
            var menu = _menuService.Get(menuId);
            var dishMenusByDates = (from dishMenu in menu.DishMenus
                    orderby dishMenu.ServiceDate
                    select menu.DishMenus
                        .Where(x => x.ServiceDate.Equals(dishMenu.ServiceDate))
                        .ToList())
                .ToList();
            for (var i = 0; i < dishMenusByDates.Count; i++)
            {
                var item = dishMenusByDates[i];
                for (var j = 0; j < item.Count; j++)
                {
                    if (item.Count == 1)
                        break;
                    dishMenusByDates.Remove(dishMenusByDates[i + 1]);
                }
            }

        var count = 1;
            var result = new List<Order>();
            while (count <= duration)
            {
                result.Add(
                    Post(new Order()
                        {
                            MenuId = menuId,
                            SchoolKidId = schoolKidId,
                            Menu = menu.ToInstance(),
                            dishes = dishMenusByDates[(index + count) % dishMenusByDates.Count].Select(x => x.Dish.ToInstance()).ToList(),
                            DishesIds = dishMenusByDates[(index + count) % dishMenusByDates.Count].Select(x => x.Dish.Id).ToList(),
                            dates = new[] { DateTimeOffset
                                .FromFileTime(DateTime.Today.AddDays((double)(dishMenusByDates[(index + count) % dishMenusByDates.Count][0].ServiceDate + (index + count) % dishMenusByDates.Count)).ToFileTimeUtc()).ToUnixTimeMilliseconds() }
                        }
                    ));
                count++;
            }

            return result;
        }
        

        public Order Put(Order order)
        {
            db.Orders.Update(order.ToEntity());
            db.SaveChanges();
            return order;
        }

        public IEnumerable<Order> Get()
        { 
            var orders = db.Orders
                .Include(x => x.Menu)
                .Include(x => x.Dishes)
                .Select(x => x.ToInstance());
            return orders;
        }

        public Order Get(string id)
        {
            var order = db.Orders.Include(x => x.Menu)
                .Include(x => x.Dishes).FirstOrDefault(o => o.Id == id);
            return order?.ToInstance(); 
        }

        public IEnumerable<Order> GetSchoolKidsOrders(string schoolKidId)
        {
            var orders = db.Orders
                .Include(x => x.Menu)
                .Include(x => x.Dishes)
                .Where(order => order.SchoolKidId == schoolKidId)
                .Select(x => x.ToInstance());
            return orders;
        }

        public Order Delete(string id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return order.ToInstance(); 
        }
    }
}
