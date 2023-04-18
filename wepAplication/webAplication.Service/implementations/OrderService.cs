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
        public Order Post(Order order)
        {
            db.Orders.Add(order.ToEntity());
            db.SaveChanges();
            return order;
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
                .Select(x => x.ToInstance());
            return orders;
        }

        public Order Get(string id)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == id)?.ToInstance();
            return order; 
        }

        public IEnumerable<Order> GetSchoolKidsOrders(string schoolKidId)
        {
            var orders = db.Orders
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
