using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        Task<BaseResponse<IEnumerable<Menu.Entity>>> Get();
        Task<BaseResponse<Menu>> GetMenu(string menuId);
/*        public Task<BaseResponse<Menu>> CreateMenu(CreateMenuViewModel createMenuViewModel);
*/
        public Task<BaseResponse<IActionResult>> DeleteMenu(string menuId);

        Task<BaseResponse<Menu>> Put(string menuId, Menu menu, string[] dishesId);
        Task<BaseResponse<IActionResult>> CreateOrder(string menuId, string[] dishIds, string schoolKidId, long[] dates);
        Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);
        Task<BaseResponse<IActionResult>> ChangeOrder(string orderId, Order order);

        Task<BaseResponse<Order>> GetOrder(string id);
        Task<BaseResponse<IEnumerable<Order>>> getSchoolKidsOrders(string schoolKidId);

    }
}
