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

        public async Task<BaseResponse<IEnumerable<Menu.Entity>>> Get()
        {
            try
            {
                var menus = await db.Menus.Include(m => m.Dishes).ToListAsync();

                if (menus == null || menus.Count() == 0)
                {
                    return new BaseResponse<IEnumerable<Menu.Entity>>
                    {
                        Description = "There is no any menus",
                        StatusCode = StatusCode.BAD
                    };
                }
                return new BaseResponse<IEnumerable<Menu.Entity>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = menus,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[Get]: {exception.Message}");
                return new BaseResponse<IEnumerable<Menu.Entity>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<IActionResult>> Put(string jsonObject)
        {
            try
            {
                var menu = Menu.FromJsonPut(jsonObject);
                var menuInDb = db.Menus.FirstOrDefault(x => x.Id == menu.ToEntity().Id);
                if (menuInDb == null)
                    return new BaseResponse<IActionResult>
                    {
                        Description = "Net takogo menu",
                        StatusCode = StatusCode.BAD
                    };
                db.Menus.Update(menu.ToEntity());
                await db.SaveChangesAsync();
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[Put]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel model)
        {
            try
            {

                var menu = db.Menus.FirstOrDefault(x => x.Id == model.menuId).ToInstance();

                var dishes = db.Dishes.Where(x => model.dishIds.Any(y=> y==x.Id)).ToList();


                if (menu == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no menu with that id",
                        StatusCode = StatusCode.BAD
                    };
                }

                if (dishes == null || dishes.Count() == 0)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no dish with that id",
                        StatusCode = StatusCode.BAD
                    };
                }
                menu.addDishes(dishes);
                db.Update(menu.ToEntity());
                db.SaveChanges();

                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }

        }

        public async Task<BaseResponse<IActionResult>> Post(string jsonObj)
        {
            try
            {
                var menu = Menu.FromJsonPost(jsonObj).ToEntity();
                db.Menus.Add(menu);
                db.SaveChanges();

                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK,
                };

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CreateMenu]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
   

        public async Task<BaseResponse<IActionResult>> Delete(string menuId)
        {
            try
            {
                var menuForDelete = db.Menus.FirstOrDefault(x => x.Id == menuId);
                if (menuForDelete == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no menu with that id",
                        StatusCode = StatusCode.BAD
                    };
                }

                db.Menus.Remove(menuForDelete);
                db.SaveChanges();

                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[DeleteMenu]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Menu.Entity>> Get(string menuId)
        {
            try
            {
                var menu = db.Menus.FirstOrDefault(m => m.Id == menuId);

                if (menu == null)
                {
                    return new BaseResponse<Menu.Entity>
                    {
                        Description = $"There is no menus with id {menuId}",
                        StatusCode = StatusCode.BAD
                    };
                }

                return new BaseResponse<Menu.Entity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = menu,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<Menu.Entity>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
    }
}