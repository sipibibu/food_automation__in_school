using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    //*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*//*
    [Route("api/[controller]")]
    public class DishesController
    {
        private IDishService _dishService;


        private readonly ILogger<DishesController> _logger;

        public DishesController(ILogger<DishesController> logger, IDishService dishService)
        {
            _logger = logger;
            _dishService = dishService;
        }


        [HttpGet]
        public async Task<BaseResponse<IEnumerable<string>>> Get()
        {
            try
            {
                var result = _dishService
                    .GetDishes()
                    .Select(x => JsonConvert.SerializeObject(x));
                return new BaseResponse<IEnumerable<string>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<string>> Get(string id)
        {
            try
            {
                var result = _dishService.GetDish(id);
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [Authorize(Roles = "canteenEmployee, admin")]
        [HttpPost]
        public async Task<BaseResponse<string>> Post(string dishJson)
        {
            try
            {
                var dish = Dish.FromJsonPost(dishJson);
                var result = _dishService.CreateDish(dish);
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = e.Message
                };
            }
            
        }


        [HttpPut]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<string>> Put(string dishJson)
        {
            try
            {
                var dish = Dish.FromJsonPut(dishJson);
                var result = _dishService.UpdateDish(dish);
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        //[HttpDelete("{id}")]
        [HttpDelete]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<string>>Delete(string id)
        {
            try
            {
                var result = _dishService.DeleteDish(id);
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
    }
}