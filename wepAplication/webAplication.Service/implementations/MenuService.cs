using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;
using wepAplication;

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

        public async Task<BaseResponse<IEnumerable<Menu>>> Get()
        {
            try
            {
                var menus = await db.Menus.Include(m => m.dishMenus).ThenInclude(dm => dm.dish).ToListAsync();

                if (menus == null || menus.Count() == 0)
                {
                    return new BaseResponse<IEnumerable<Menu>>
                    {
                        Description = "There is no any menus",
                        StatusCode = StatusCode.BAD
                    };
                }
                return new BaseResponse<IEnumerable<Menu>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = menus,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<IEnumerable<Menu>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Menu>> Put(string menuId, Menu menu, string[] dishesId)
        {
            try
            {
                var oldmenu = await db.Menus.FirstOrDefaultAsync(m => m.Id == menuId);

                if (oldmenu == null)
                {
                    return new BaseResponse<Menu>()
                    {
                        StatusCode = StatusCode.BAD,
                    };
                }

                oldmenu.title = menu.title;
                oldmenu.description = menu.description;
                oldmenu.timeToService = menu.timeToService;

                oldmenu.dishMenus.Clear();
                foreach (var dishId in dishesId)
                {
                    oldmenu.dishMenus.Add(new DishMenu() { DishId = dishId, MenuId = menuId });
                }

                db.Menus.Update(oldmenu);
                await db.SaveChangesAsync();

                return new BaseResponse<Menu>()
                {
                    StatusCode = StatusCode.OK,
                    Data = oldmenu,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<Menu>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IActionResult>> AddExistingDishToMenu(AddExistingDishToMenuViewModel addExistingDishToMenuViewModel)
        {
            try
            {
                Menu? menu = await db.Menus.Include(m => m.dishMenus).ThenInclude(dm => dm.dish).FirstOrDefaultAsync(x => x.Id == addExistingDishToMenuViewModel.menuId);
                if (menu == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no menu with that id",
                        StatusCode = StatusCode.BAD
                    };
                }


                var dishes = db.Dishes.Where(x => addExistingDishToMenuViewModel.dishIds.Contains(x.Id));
                if (dishes == null || dishes.Count() == 0)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no dish with that id",
                        StatusCode = StatusCode.BAD
                    };
                }

                foreach (var dish in dishes)
                {
                    var dishMenu = new DishMenu()
                    {
                        DishId = dish.Id,
                        MenuId = addExistingDishToMenuViewModel.menuId,
                        dish = dish,
                        menu = menu,
                    };

                    dish.dishMenus.Add(dishMenu);
                    menu.dishMenus.Add(dishMenu);
                }
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

        public async Task<BaseResponse<IActionResult>> CreateMenu(CreateMenuViewModel createMenuViewModel)
        {
            try
            {
                var menu = new Menu()
                {
                    title = createMenuViewModel.Title,
                    description = createMenuViewModel.Description,
                    timeToService = createMenuViewModel.timeToService,
                };

                db.Menus.AddAsync(menu);
                db.SaveChangesAsync();

                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK
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

        public async Task<BaseResponse<IActionResult>> DeleteMenu(string menuId)
        {
            try
            {
                Menu? menuForDelete = await db.Menus.FirstOrDefaultAsync(x => x.Id == menuId);
                if (menuForDelete == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        Description = "there is no menu with that id",
                        StatusCode = StatusCode.BAD
                    };
                }

                db.Menus.Remove(menuForDelete);
                await db.SaveChangesAsync();

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

        public async Task<BaseResponse<Menu>> GetMenu(string menuId)
        {
            try
            {
                var menu = db.Menus.Include(m => m.dishMenus).ThenInclude(dm => dm.dish).FirstOrDefault(m => m.Id == menuId);

                if (menu == null)
                {
                    return new BaseResponse<Menu>
                    {
                        Description = "There is no any menus",
                        StatusCode = StatusCode.BAD
                    };
                }

                return new BaseResponse<Menu>()
                {
                    StatusCode = StatusCode.OK,
                    Data = menu,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[AddExistingDishToMenu]: {exception.Message}");
                return new BaseResponse<Menu>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IActionResult>> CreateOrder(string menuId, string[] dishIds, string schoolKidId)
        {
            try
            {
                var menu = await db.Menus.FirstOrDefaultAsync(x => x.Id == menuId);

                if (menuId == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "there is no menu with that id"
                    };
                }

                foreach (var dishId in dishIds)
                {
                    //dobavit' proverky na to chto dish in that menu
                    var dish = db.Dishes.FirstOrDefaultAsync(x => x.Id == dishId);
                    if (dish == null)
                    {
                        return new BaseResponse<IActionResult>()
                        {
                            StatusCode = StatusCode.BAD,
                            Description = $"There is no dish with that id: {dishId}"
                        };
                    }
                }

                var order = new Order()
                {
                    MenuId = menuId,
                    DishIds = dishIds.ToList(),
                    SchoolKidId = schoolKidId,
                };

                await db.Orders.AddAsync(order);
                db.SaveChanges();

                return new BaseResponse<IActionResult>()
                {
                    StatusCode= StatusCode.OK
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

        public async Task<BaseResponse<IActionResult>> ChangeOrder(string orderId,Order order)
        {
            try
            {
                var oldOrder = await db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (oldOrder == null)
                {
                    return new BaseResponse<IActionResult>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no order with that id:{orderId}"
                    };
                }
                oldOrder.Update(order);
                db.Orders.Update(oldOrder);
                db.SaveChanges();
                return new BaseResponse<IActionResult>()
                {
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[ChangeOrder]: {exception.Message}");
                return new BaseResponse<IActionResult>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }

        }

        public async Task<BaseResponse<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var orders = await db.Orders.ToListAsync();
                return new BaseResponse<IEnumerable<Order>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = orders,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<IEnumerable<Order>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Order>> GetOrder(string id)
        {
            try
            {
                var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                {
                    return new BaseResponse<Order>()
                    {
                        StatusCode = StatusCode.OK,
                        Data = null,
                    };
                }
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.OK,
                    Data = order,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<Order>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Order>>> getSchoolKidsOrders(string schoolKidId)
        {
            try
            {
                var schoolKid = await db.SchoolKids.FirstOrDefaultAsync(sk => sk.Id == schoolKidId);
                if (schoolKid == null)
                {
                    return new BaseResponse<IEnumerable<Order>>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no schoolKid with that id: {schoolKidId}"
                    };
                }

                var orders = db.Orders.Where(order => order.SchoolKidId == schoolKidId).ToList();
                return new BaseResponse<IEnumerable<Order>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = orders,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetOrders]: {exception.Message}");
                return new BaseResponse<IEnumerable<Order>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
    }
}
