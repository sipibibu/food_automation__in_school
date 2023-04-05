using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        Task<BaseResponse<IEnumerable<Menu.Entity>>> Get();
        Task<BaseResponse<Menu.Entity>> Get(string menuId);
        public Task<BaseResponse<IActionResult>> Post(string jsonObj);

        public Task<BaseResponse<IActionResult>> Delete(string menuId);
        Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);

        Task<BaseResponse<IActionResult>> Put(string jsonObject);
    }
}
