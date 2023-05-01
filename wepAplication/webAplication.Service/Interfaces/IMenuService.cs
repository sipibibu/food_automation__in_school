using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<Menu.Entity> Get();
        string GetAsJson(Menu.Entity menu);
        IEnumerable<string> GetAsJson(List<Menu.Entity> menuse);

        Menu.Entity Get(string menuId);
        Menu Post(Menu jsonObj);
        Menu Post(BuffetMenu jsonObj);
        Menu Put(Menu jsonObject);
        Menu SetDishDates(string menuId, string dishId, IEnumerable<long> dates);
        Menu Delete(string menuId);
        Menu  AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);
    }
}
