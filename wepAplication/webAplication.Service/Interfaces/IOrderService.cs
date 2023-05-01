using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IOrderService
    {
        Order Get(string id);
        IEnumerable<Order> Get();
        IEnumerable<Order> GetSchoolKidsOrders(string schoolKidId);
        IEnumerable<Order> OrderMenu(string schoolKidId, string menuId, int duration);
        Order Post(Order order);
        Order Put(Order order);
        Order Delete(string id);
    }
}
