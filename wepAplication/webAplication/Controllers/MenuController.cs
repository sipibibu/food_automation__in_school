using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("")]

        public async Task<BaseResponse<IEnumerable<string>>> GetAll()
        {
            try
            {
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = _menuService.Get().Select(x => JsonConvert.SerializeObject(x)) 
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

        [HttpPost]
/*        [Route("")]
        [Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> Post(string jsonObj)
        {
            try
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_menuService.Post(jsonObj))
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpPut]
        [Route("[action]")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> AddExistingDishToMenu(AddExistingDishToMenuViewModel jsonObj)
        {
            try
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_menuService.AddExistingDishToMenu(jsonObj)) 
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpDelete]
        [Route("{id}")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_menuService.Delete(id)) 
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpPut]
        [Route("{id}")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> Put(string jsonObj)
        {
            try
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_menuService.Put(jsonObj)) 
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        
        [HttpGet]
        [Route("{id}")]
        public async Task<BaseResponse<string>> Get(string id)
        {
            try
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_menuService.Get(id)) 
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD, 
                    Description = e.Message
                };
            }
        }
    }
}
