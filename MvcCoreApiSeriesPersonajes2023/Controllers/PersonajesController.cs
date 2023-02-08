using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes2023.Helpers;
using MvcCoreApiSeriesPersonajes2023.Models;
using MvcCoreApiSeriesPersonajes2023.Services;

namespace MvcCoreApiSeriesPersonajes2023.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeries service;
        private HelperPathProvider helperPath;
        private ServiceStorageBlobs serviceStorage;
        public PersonajesController(ServiceSeries service,HelperPathProvider helper, ServiceStorageBlobs serviceStorage) 
        {
            this.service = service;
            this.helperPath = helper;
            this.serviceStorage = serviceStorage;
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
        public async Task<IActionResult> CreatePersonaje(Personaje personaje, IFormFile archivo)
        {
            //Debemos subir el fichero a nuestro servidor
            string fileName = archivo.FileName;
            //Subijmos el fichero a azure y extraemos la url
            string urlBlob = "";
            using (Stream stream = archivo.OpenReadStream())
            {
                urlBlob = await this.serviceStorage.UploadBlobAsync(fileName, stream);
            }
            //Guardamos en la clase Personaje la URL del Blob de imagen
            personaje.Imagen = urlBlob;

            //Guardamos el personaje en la API
            await this.service.CreatePersonajeAsync(personaje);
            //Vamos a volver a la vista de listado de personajes
            return RedirectToAction("ListadoPersonajes", new { idserie = personaje.IdSerie });
        }

        //[HttpPost] METODO PARA GUARDAR EN EL SERVER DE LA APP
        //public async Task<IActionResult> CreatePersonaje(Personaje personaje, IFormFile archivo)
        //{
        //    //Debemos subir el fichero a nuestro servidor
        //    string fileName = archivo.FileName;
        //    string path = this.helperPath.GetMapPath(Folders.Images,fileName);
        //    using(Stream stream = new FileStream(path, FileMode.Create))
        //    {
        //        await archivo.CopyToAsync(stream);
        //    }
        //    //Por ultimo, guardamos en el api la url de nuestro servidor de la imagen del personaje
        //    string folder = this.helperPath.GetNameFolder(Folders.Images);
        //    personaje.Imagen = this.helperPath.GetHostUrl() + folder+ "/" + fileName;

        //    await this.service.CreatePersonajeAsync(personaje);
        //    //Vamos a volver a la vista de listado de personajes
        //    return RedirectToAction("ListadoPersonajes", new { idserie = personaje.IdSerie });
        //}

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
