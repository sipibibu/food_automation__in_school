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


        public MenuService(AplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<Menu.Entity> Get()
        {
            var menus = db.Menus.Include(m => m.Dishes).ToList();
            return menus;
        }
        public Menu Put(Menu menu)
        {
            var menuInDb = db.Menus.FirstOrDefault(x => x.Id == menu.ToEntity().Id);
            if (menuInDb == null)
                throw new Exception("Net menu s takim id");
            var dishes = db.Dishes.Where(x => menu.GetDishesIds().Any(y => y == x.Id)).Select(z=>z.Id).ToList();
            if (dishes.Count == 0)
                throw new Exception("Net takih ublud");

            menu.ChangeDihseIds(dishes);
            menuInDb = menu.ToEntity();
            db.ChangeTracker.Clear();

            db.Menus.Update(menuInDb);
            db.SaveChanges();
            return menu;
        }
        public  Menu AddExistingDishToMenu(AddExistingDishToMenuViewModel model)
        {
            var menuEntity = db.Menus.FirstOrDefault(x => x.Id == model.menuId);
            if (menuEntity == null)
            {
                throw new Exception("Net menu s takim id");
            }
            var menu = menuEntity.ToInstance();
            var dishes = db.Dishes.Where(x => model.dishIds.Any(y=> y==x.Id)).ToList();
            if (dishes.Count == 0)
                throw new Exception("Net takih blud");

            menu.addDishes(dishes);
            db.ChangeTracker.Clear();
            db.Update(menu.ToEntity());
            db.SaveChanges();

            return menu;

        }

        public Menu Post(Menu menu)
        {
            var menuEntity = menu.ToEntity();
            var dishesIds=db.Dishes.Where(y=>menuEntity.DishesIds.Any(x=>x==y.Id)).Select(x=>x.Id).ToList();

            if (dishesIds != null)
            {
                menuEntity.DishesIds = dishesIds;
            }

            db.Menus.Add(menuEntity);
            db.SaveChanges();

            return menuEntity.ToInstance();
        }
   

        public Menu Delete(string menuId)
        {
            var menuForDelete = db.Menus.FirstOrDefault(x => x.Id == menuId);
            if (menuForDelete != null)
            {
                db.Menus.Remove(menuForDelete);
                db.SaveChanges();
                return menuForDelete.ToInstance();
            }
            throw new Exception("Net takogo menu");
        }

        public Menu Get(string menuId)
        {
            var menu = db.Menus.FirstOrDefault(m => m.Id == menuId);
            if (menu == null)
            {
                throw new Exception("Net menu s takim id");
            }
            return menu.ToInstance();
            
        }
    }
}