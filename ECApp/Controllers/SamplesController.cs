using ECApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECApp.Controllers
{
    [Authorize]
    public class SamplesController : Controller
    {
        protected readonly IAuthorizationService authService;

        public SamplesController(IAuthorizationService authorization)
        {
            authService = authorization;
        }

        public IActionResult Index()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            return View("Detail",GenerateFilm(DateTime.Now.Second));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (!id.HasValue) return NotFound();

            Film film = GenerateFilm(id.Value);

            var authorizedFilm = await AuthorizeFilm(film);

            if (!authorizedFilm) return new ForbidResult();

            return View(film);
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        //[Authorize("AdminPolicy")]
        [Authorize("HasValidEmail")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            Film film = GenerateFilm(id.Value);

            return View(film);
        }


        [HttpPost]
        public IActionResult Edit([FromForm]Film film)
        {

            TempData["Mensaje"] = "Film (casi) guardado correctamente.";
            return RedirectToAction("Index");
        }

        private Film GenerateFilm(int seed)
        {
            return new Film
            {
                Id = seed,
                Director = $"Director_{seed}",
                Year = Convert.ToInt16(seed + 1950),
                ImdbLink = $"Link_{seed}",
                ImdbScore = 7 + (seed % 10) / 10,
                Keywords = new List<string> { $"Keyword_{seed}" },
                Title = $"Title_{seed}"
            };
        }

        protected async virtual Task<bool> AuthorizeFilm(Film film)
        {
            var authorizationResult = await authService.AuthorizeAsync(User, film, "FilmPolicy");

            return authorizationResult.Succeeded;

        }
    }
}
