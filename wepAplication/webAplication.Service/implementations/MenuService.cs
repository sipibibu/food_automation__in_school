using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.implementations
{
    public class MenuService : IMenuService
    {
        private AplicationDbContext db;
        private readonly ILogger<MenuService> _logger;


        public MenuService(ILogger<MenuService> logger, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
        }

        public IEnumerable<Menu> Get()
        {
            return db.Menus.Include(m => m.Dishes).Select(x => x.ToInstance()).ToList();
        }
        public Menu Put(string jsonObject)
        {
            var menu = Menu.FromJsonPut(jsonObject);
            db.Menus.Update(menu.ToEntity());
            db.SaveChanges();
            return menu;
        }
        public Menu AddExistingDishToMenu(AddExistingDishToMenuViewModel model)
        {
            var menu = Get(model.menuId);
            var dishes = db.Dishes.Where(x => model.dishIds.Any(y=> y==x.Id)).ToList();
            menu.addDishes(dishes);
            db.Update(menu.ToEntity());
            db.SaveChanges();
            return menu;
        }

        public Menu Post(string jsonObj)
        {
            var menu = Menu.FromJsonPost(jsonObj);
            db.Menus.Add(menu?.ToEntity());
            db.SaveChanges();
            return menu;
        }

        public Menu Delete(string menuId)
        {
            var menu = Get(menuId);
            db.Menus.Remove(menu.ToEntity());
            db.SaveChanges();
            return menu;
        }

        public  Menu Get(string menuId)
        {
            return db.Menus.FirstOrDefault(m => m.Id == menuId)?.ToInstance(); 
        }
    }
}