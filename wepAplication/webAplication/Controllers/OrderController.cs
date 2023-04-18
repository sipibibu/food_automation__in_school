using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Controllers
{
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost]
        [Route("[action]")]
        public BaseResponse<Order> Post(string orderJson)
        {
            try
            {
                var order = Order.FromJsonPost(orderJson);
                var result = _orderService.Post(order);
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpPut]
        [Route("[action]")]
        public BaseResponse<Order> Put(string orderJson)
        {
            try
            {
                var order = Order.FromJsonPut(orderJson);
                var result = _orderService.Put(order);
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<BaseResponse<string>> Get(string id)
        {
            try
            {
                var result = _orderService.Get(id);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result),
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            } }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<string>>> Get()
        {
            try
            {
                var result = _orderService
                    .Get()
                    .Select(x => JsonConvert.SerializeObject(x));
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<string>>> GetSchoolKidsOrders(string schoolKidId)
        {
            try
            {
                var result = _orderService
                    .GetSchoolKidsOrders(schoolKidId)
                    .Select(x => JsonConvert.SerializeObject(x));
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
        
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<Order>> Delete(string orderId)
        {
            try
            {
                var result = _orderService.Delete(orderId);
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = Domain.StatusCode.OK, 
                    Description = e.Message
                };
            }
        }
    }
}
