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
        Task<BaseResponse<Order.Entity>> Get(string id);
        Task<BaseResponse<IEnumerable<Order.Entity>>> Get();
        Task<BaseResponse<IEnumerable<Order.Entity>>> GetSchoolKidsOrders(string schoolKidId);

        Task<BaseResponse<IActionResult>> Post(string jsonObj);
        Task<BaseResponse<IActionResult>> Put(string jsonObj);
        Task<BaseResponse<IActionResult>> Delete(string id);
    }
}
