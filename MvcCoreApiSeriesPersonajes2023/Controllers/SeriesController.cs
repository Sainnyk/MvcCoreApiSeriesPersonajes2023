using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes2023.Models;
using MvcCoreApiSeriesPersonajes2023.Services;

namespace MvcCoreApiSeriesPersonajes2023.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceSeries service;
        public SeriesController(ServiceSeries service)
        {
            this.service = service;
        }
        
        public async Task<IActionResult> ListadoSeries()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        public async Task<IActionResult> Details(int idserie)
        {
            Serie serie = await this.service.GetSerieIdAsync(idserie);
            return View(serie);
        }
    }
}
