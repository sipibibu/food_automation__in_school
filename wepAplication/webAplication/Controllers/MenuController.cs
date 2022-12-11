using Microsoft.AspNetCore.Authorization;
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
        [Route("")]

        public async Task<BaseResponse<IEnumerable<Menu>>> Get() ///govnishe
        {
            var response = await _menuService.Get();
            if (response.StatusCode == Domain.StatusCode.OK)
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
        [Route("")]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<IEnumerable<Menu>>> CreateMenu(CreateMenuViewModel createMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _menuService.CreateMenu(createMenuViewModel);
                if (response.StatusCode == Domain.StatusCode.OK)
                {
                    return new BaseResponse<IEnumerable<Menu>>()
                    {
                        StatusCode = Domain.StatusCode.OK,
                    };
                }
            }
            return new BaseResponse<IEnumerable<Menu>>()
            {
                StatusCode = Domain.StatusCode.BAD,
            };
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(addExistingDishToMenuViewModel.menuId) || addExistingDishToMenuViewModel.dishIds == null || addExistingDishToMenuViewModel.dishIds.Length == 0)
                    return new BaseResponse<IActionResult>()
                    {
                        Description = (string.IsNullOrEmpty(addExistingDishToMenuViewModel.menuId) ? "menu id is null or empty" : "") + "\n" + (addExistingDishToMenuViewModel.dishIds == null || addExistingDishToMenuViewModel.dishIds.Length == 0 ? "dish id is null or empty" : ""),
                        StatusCode = Domain.StatusCode.BAD
                    };

                var response = await _menuService.AddExistingDishToMenu(addExistingDishToMenuViewModel);
                if (response != null)
                    return response;
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "Response was null"
                };
            }
            return new BaseResponse<IActionResult>()
            {
                StatusCode = Domain.StatusCode.BAD,
                Description = "Response was null"
            };
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<IActionResult>> Delete(string id)
        {
            return await _menuService.DeleteMenu(id);

        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "canteenEmploee, admin")]
        public async Task<BaseResponse<Menu>> Put(string id, MenuPutViewModel menuPutViewModel)
        {
            return await _menuService.Put(id, menuPutViewModel.Menu, menuPutViewModel.DishIds);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> CreateOrder(CreateOrderViewModel model)
        {
            return await _menuService.CreateOrder(model.menuId, model.dishIds, model.SchoolKidId);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<IActionResult>> ChangeOrder(string orderId, Order order)
        {
            return await _menuService.ChangeOrder(orderId, order);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Order>> GetOrder(string id)
        {
            return await _menuService.GetOrder(id);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Order>>> getSchoolKidsOrders(string schoolKidId)
        {
            return await _menuService.getSchoolKidsOrders(schoolKidId);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BaseResponse<Menu>> GetMenu(string id)
        {
            var response = await _menuService.GetMenu(id);
            if (response.StatusCode == Domain.StatusCode.OK)
            {
                response.Data.dishes = response.Data.dishMenus.Select(x => x.dish);
                return new BaseResponse<Menu>()
                {
                    StatusCode = response.StatusCode,
                    Data = response.Data
                };
            }

            else
                return response;
        }

    }
}
