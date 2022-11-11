using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using webAplication.Domain;
using webAplication.Service.implementations;
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
        [Route("[action]")]

        public async Task<BaseResponse<IEnumerable<Menu>>> Get() ///govnishe
        {
            var response = await _menuService.Get();
            if (response.StatusCode == Domain.Interfaces.StatusCode.OK)
                return new BaseResponse<IEnumerable<Menu>>()
                {
                    StatusCode = response.StatusCode,
                    Data = response.Data.Select(x =>
                    {
                        x.dishes = x.dishMenus.Select(x => x.dish);
                        return x;
                    })
                };
            else
                return response;
        }

        [HttpPost]
        [Route("[action]")]

        public async Task<BaseResponse<IEnumerable<Menu>>> CreateMenu(CreateMenuViewModel createMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _menuService.CreateMenu(createMenuViewModel);
                if (response.StatusCode == Domain.Interfaces.StatusCode.OK)
                {
                    return new BaseResponse<IEnumerable<Menu>>()
                    {
                        StatusCode = Domain.Interfaces.StatusCode.OK,
                    };
                }
            }
            return new BaseResponse<IEnumerable<Menu>>()
            {
                StatusCode = Domain.Interfaces.StatusCode.BAD,
            };
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(addExistingDishToMenuViewModel.menuId) || addExistingDishToMenuViewModel.dishIds == null || addExistingDishToMenuViewModel.dishIds.Length == 0)
                    return new BaseResponse<IActionResult>()
                    {
                        Description = (string.IsNullOrEmpty(addExistingDishToMenuViewModel.menuId) ? "menu id is null or empty" : "") + "\n" + (addExistingDishToMenuViewModel.dishIds == null || addExistingDishToMenuViewModel.dishIds.Length == 0 ? "dish id is null or empty" : ""),
                        StatusCode = Domain.Interfaces.StatusCode.BAD
                    };

                var response = await _menuService.AddExistingDishToMenu(addExistingDishToMenuViewModel);
                if (response != null)
                    return response;
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = Domain.Interfaces.StatusCode.BAD,
                    Description = "Response was null"
                };
            }
            return new BaseResponse<IActionResult>()
            {
                StatusCode = Domain.Interfaces.StatusCode.BAD,
                Description = "Response was null"
            };
        }
    }
}
