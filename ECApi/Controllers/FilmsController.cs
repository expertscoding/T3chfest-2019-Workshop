using System.Collections.Generic;
using System.Linq;
using ECApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FilmsController : ControllerBase
    {
        private readonly CinemaDbContext context;

        // GET api/values
        public FilmsController(CinemaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Film>> Get() => context.Films.OrderByDescending(f => f.ImdbScore).Take(50).ToList();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            return context.Films.Find(id);
        }

        [HttpPost]
        [Authorize("DAY_1")]
        [Authorize]
        public IActionResult Post(Film film)
        {

            if (ModelState.IsValid)
            {
                context.Films.Add(film);

                //Don't want to populate db with lot of films
                // To write in DB, just uncomment next line
                //context.SaveChanges();

                return Ok(film);
            }

            return BadRequest();
        }

        [HttpPatch]
        [Authorize("DAY_9")]
        public IActionResult Patch(Film film)
        {

            //Model will only contains informed changed fields so we don´t validate model

            // Get Film from DB
            var entity = context.Films.Find(film.Id);

            if (entity == null) return NotFound();

            foreach(var prop in typeof(Film).GetProperties())
            {
                if (prop.Name == "Id") continue;

                object value = prop.GetValue(film);

                if (value != null)
                {
                    prop.SetValue(entity, value);
                }

            }

            //Don't want to populate db with lot of films
            // To write in DB, just uncomment next line
            //context.SaveChanges();

            return Ok(entity);
            

            
        }
    }
}
