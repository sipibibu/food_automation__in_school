using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Packaging;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Service.implementations
{
    public class MenuService : IMenuService
    {
        private AplicationDbContext db;
        private IDishService _dishService;


        public MenuService(AplicationDbContext context, IDishService dishService)
        {
            db = context;
            _dishService = dishService;
        }

        public IEnumerable<Menu.Entity> Get()
        {
            var menus = db.Menus.Include(x => x.DishMenus).Include(m => m.Dishes).ToList();
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

        public IEnumerable<DishMenu.Entity> GetDishesDates(Menu.Entity menu)
        {
            return menu.DishMenus;
        }

        public Menu SetDishDates(string menuId, string dishId, IEnumerable<long> dates)
        {
            //метод для выставления время подачи блюда в плановом меню
            var menu = Get(menuId);
            var dish = _dishService.GetDish(dishId);
            menu.DishMenus = menu.DishMenus.Where(x => !x.DishId.Equals(dishId)).ToList();
            db.SaveChanges();
            menu.DishMenus.AddRange(dates.Select(x => new DishMenu.Entity(menu, dish, x)).ToList());
            db.SaveChanges();
            return menu.ToInstance();
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
        
        public Menu Post(BuffetMenu menu)
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

        public Menu.Entity Get(string menuId)
        {
            var menu = db.Menus
                .Include(x => x.Dishes)
                .Include(x => x.DishMenus)
                .FirstOrDefault(m => m.Id == menuId);
            if (menu == null)
            {
                throw new Exception("Net menu s takim id");
            }
            return menu;
        }

        // public string GetAsJson(Menu.Entity menu)
        // {
        //     if (menu is BuffetMenu.Entity)
        //         return JsonConvert.SerializeObject((menu as BuffetMenu.Entity).ToInstance());
        //     return JsonConvert.SerializeObject(menu.ToInstance());
        // }
        // public IEnumerable<string> GetAsJson(List<Menu.Entity> menuse)
        // {
        //     return menuse.Select(x => JsonConvert.SerializeObject(x is BuffetMenu.Entity ? (x as BuffetMenu.Entity).ToInstance()
        //         : x.ToInstance()));
        // }
    }
}