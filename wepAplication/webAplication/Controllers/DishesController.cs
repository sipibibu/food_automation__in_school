using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAplication.DAL;
using wepAplication;

namespace webAplication.Controllers
{
    [ApiController]
    /*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*/
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        AplicationDbContext db;

        private readonly ILogger<DishesController> _logger;

        public DishesController(ILogger<DishesController> logger, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> Get()
        {
            return await db.Dishes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> Get(string id)
        {
            Dish? dish = await db.Dishes.FirstOrDefaultAsync(x => x.Id == id);
            if (dish == null)
                return NotFound();
            return new ObjectResult(dish);
        }

        [Authorize(Roles = "canteenEmployee, admin")]
        [HttpPost]
        public async Task<ActionResult<Dish>> Post(Dish dish)
        {
            if (dish == null)
            {
                return BadRequest();
            }
            db.Dishes.Add(dish);
            await db.SaveChangesAsync();
            return Ok(dish);
        }


        [Authorize(Roles = "canteenEmploee, admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Dish>> Put(string id, Dish dish) //pizedsadas
        {
            var oldDish = await db.Dishes.FirstOrDefaultAsync(x => x.Id == id);
            if (dish == null)
            {
                return BadRequest();
            }
            if (!db.Dishes.Any(x => x.Id == id))
            {
                return NotFound();
            }

            oldDish.dishMenus = dish.dishMenus;
            oldDish.title = dish.title;
            oldDish.description = dish.Description;
            oldDish.price = dish.price;
            oldDish.imageId = dish.imageId;

            db.Update(oldDish);
            await db.SaveChangesAsync();
            return Ok(oldDish);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "canteenEmploee, admin")]

        public async Task<ActionResult<Dish>> Delete(string id)
        {
            var dish = db.Dishes.FirstOrDefault(x => x.Id == id);
            if (dish == null)
            {
                return BadRequest();
            }

            db.Remove(dish);
            await db.SaveChangesAsync();
            return Ok(dish);
        }

    }
}
