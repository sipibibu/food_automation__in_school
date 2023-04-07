using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using Newtonsoft.Json;
using webAplication.Service.implementations;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuService> _logger;
        public MenuController(IMenuService menuService,ILogger<MenuService> logger)
        {
            _menuService = menuService;
            _logger = logger;

        }
        [HttpPost]
        [Route("")]
        /*  [Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> Post(string jsonObj)
        {
            try
            {
                var res = _menuService.Post(Menu.FromJsonPost(jsonObj));
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CreateMenu]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }

        [HttpPut]
        [Route("")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> Put(string jsonObj)
        {
            try
            {
                var res = _menuService.Put(Menu.FromJsonPut(jsonObj));
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[Put]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }

        }


        [HttpGet]
        [Route("{id}")]
        public async Task<BaseResponse<string>> Get(string id)
        {
            try
            {
                var res = _menuService.Get(id);
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[Get]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<BaseResponse<string>> GetAll()
        {
            try
            {
                var res=_menuService.Get();
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetAll]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }


        [HttpPut]
        [Route("[action]")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<string>> AddExistingDishToMenu(AddExistingDishToMenuViewModel model)
        {
            try
            {
                var res= _menuService.AddExistingDishToMenu(model);
                return new BaseResponse<string>()
                {
                    Data=JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
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
                 var res=_menuService.Delete(id);
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(res),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[DeleteMenu]: {exception.Message}");
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }

        }


    }
}
