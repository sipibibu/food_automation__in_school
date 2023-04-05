using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<BaseResponse<IEnumerable<Menu.Entity>>> GetAll()
        {
            return await _menuService.Get();
        }

        [HttpPost]
/*        [Route("")]
        [Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<IActionResult>> Post(string jsonObj)
        {
                return await _menuService.Post(jsonObj);
        }

        [HttpPut]
        [Route("[action]")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel jsonObj)
        {
            /*var model=AddExistingDishToMenuViewModel.FromJson(jsonObj);
            if (model == null)
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = Domain.StatusCode.BAD
                };*/
            return await _menuService.AddExistingDishToMenu(jsonObj);
        }

        [HttpDelete]
        [Route("{id}")]
        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<IActionResult>> Delete(string id)
        {
            return await _menuService.Delete(id);

        }

        [HttpPut]
/*        [Route("{id}")]
*/        /*[Authorize(Roles = "canteenEmploee, admin")]*/
        public async Task<BaseResponse<IActionResult>> Put(string jsonObj)
        {
            return await _menuService.Put(jsonObj);
        }

        
        [HttpGet]
        [Route("{id}")]
        public async Task<BaseResponse<Menu.Entity>> Get(string id)
        {
            return await _menuService.Get(id);
        }

    }
}
