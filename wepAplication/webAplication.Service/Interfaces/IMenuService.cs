using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<Menu.Entity> Get();
        Menu Get(string menuId);
        Menu Post(Menu jsonObj);
        Menu Put(Menu jsonObject);
        Menu Delete(string menuId);
        Menu  AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);
    }
}
