using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes2023.Models;
using MvcCoreApiSeriesPersonajes2023.Services;

namespace MvcCoreApiSeriesPersonajes2023.ViewComponents
{
    public class MenuSeriesViewComponent : ViewComponent
    {
        private ServiceSeries service;

        public MenuSeriesViewComponent(ServiceSeries service)
        {
            this.service = service;

        }

        //El metodo InvokeAsync() (siempre debe de estar) es el encargado de enviar un model hacia nuestro layout
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }
    }
}
