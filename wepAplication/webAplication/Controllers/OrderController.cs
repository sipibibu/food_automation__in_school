using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Controllers
{
    public class OrderController : ControllerBase
    {
        private IOrderService _menuService;
        public OrderController(IOrderService menuService)
        {
            _menuService = menuService;
        }
        /*   [HttpPost]
           [Route("[action]")]
           public async Task<BaseResponse<IActionResult>> Post(CreateOrderViewModel model)
           {
               return await _menuService.Post(jsonObj);
           }*/
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> Post(string jsonObj)
        {
            return await _menuService.Post(jsonObj);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> Put(string orderJson)
        {
            return await _menuService.Put(orderJson);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Order.Entity>> Get(string id)
        {
            return await _menuService.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Order.Entity>>> GetAll()
        {
            return await _menuService.Get();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Order.Entity>>> GetSchoolKidsOrders(string schoolKidId)
        {
            return await _menuService.GetSchoolKidsOrders(schoolKidId);
        }


        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> Delete(string orderId)
        {
            return await _menuService.Delete(orderId);
        }

    }
}
