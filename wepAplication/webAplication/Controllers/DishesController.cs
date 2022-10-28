using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAplication.DAL;
using wepAplication;

namespace webAplication.Controllers
{
    [ApiController]
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
        public async Task<ActionResult<Dish>> Get(Guid id)
        {
            Dish dish = await db.Dishes.FirstOrDefaultAsync(x => x.Id == id);
            if (dish == null)
                return NotFound();
            return new ObjectResult(dish);
        }

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
        [HttpPut]
        public async Task<ActionResult<Dish>> Put(Dish dish)
        {
            if (dish == null)
            {
                return BadRequest();
            }
            if (!db.Dishes.Any(x => x.Id ==dish.Id))
            {
                return NotFound();
            }

            db.Update(dish);
            await db.SaveChangesAsync();
            return Ok(dish);
        }
    }
}
