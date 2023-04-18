using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<Menu> Get();
        Menu Get(string menuId);
        public Menu Post(string jsonObj);

        public Menu Delete(string menuId);
        Menu AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel);

        Menu Put(string jsonObject);
    }
}
