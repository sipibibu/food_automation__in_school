using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        public Task<BaseResponse<IEnumerable<Menu>>> Get();
        public Task<BaseResponse<IActionResult>> GetMenu(string menuId);
        public Task<BaseResponse<IActionResult>> CreateMenu(CreateMenuViewModel createMenuViewModel);

        public Task<BaseResponse<IActionResult>> DeleteMenu(string menuId);

        Task<BaseResponse<Menu>> Put(string menuId, Menu menu, string[] dishesId);

        public Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);

    }
}
