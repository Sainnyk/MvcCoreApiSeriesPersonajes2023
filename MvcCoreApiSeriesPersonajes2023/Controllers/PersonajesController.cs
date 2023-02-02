using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes2023.Models;
using MvcCoreApiSeriesPersonajes2023.Services;

namespace MvcCoreApiSeriesPersonajes2023.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeries service;
        public PersonajesController(ServiceSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> ListadoPersonajes(int idserie)
        {
            List<Personaje> personajes = await this.service.GetPersonajesSerieAsync(idserie);
            ViewData["ID"] = idserie;
            return View(personajes);
        }

        public async Task<IActionResult> CreatePersonaje()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personaje personaje)
        {
            await this.service.CreatePersonajeAsync(personaje);
            //Vamos a volver a la vista de listado de personajes
            return RedirectToAction("ListadoPersonajes", new { idserie = personaje.IdSerie });
        }

        public async Task<IActionResult> UpdatePersonajeSerie()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewData["PERSONAJES"] = personajes;
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonajeSerie(int idpersonaje, int idserie)
        {
            await this.service.UpdateSeriePersonajeAsync(idpersonaje, idserie);
            return RedirectToAction("ListadoPersonajes", new { idserie = idserie });
        }
    }
}
