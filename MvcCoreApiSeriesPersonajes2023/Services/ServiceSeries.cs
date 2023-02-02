using MvcCoreApiSeriesPersonajes2023.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiSeriesPersonajes2023.Services
{
    public class ServiceSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceSeries(string url)
        {
            this.UrlApi = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> DatosApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T datos = await response.Content.ReadAsAsync<T>();
                    return datos;
                }
                else
                {
                    return default(T);
                }
            }
        }

        #region Metodos Series
        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series = await this.DatosApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<Serie> GetSerieIdAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.DatosApiAsync<Serie>(request);
            return serie;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int idserie)
        {
            string request = "/api/series/personajesserie/" + idserie;
            List<Personaje> personajes = await this.DatosApiAsync<List<Personaje>>(request);
            return personajes;
        }
        #endregion

        #region Metodos Personajes
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes= await this.DatosApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task<Personaje> GetPersonajeIdAsync(int id)
        {
            string request = "/api/personajes/"+id;
            Personaje personaje = await this.DatosApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task UpdateSeriePersonajeAsync(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/"+idpersonaje+"/"+idserie;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                //Aunque no creemos nada debemos enviar un objeto vacio Content en la peticion del Put

                StringContent content = new StringContent("", Encoding.UTF8, "application/json");

                await client.PutAsync(request, content);

            }
        }
        public async Task CreatePersonajeAsync(Personaje charac)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                Personaje personaje = new Personaje();
                personaje.Nombre = charac.Nombre;
                personaje.IdPersonaje = charac.IdPersonaje;
                personaje.Imagen = charac.Imagen;
                personaje.IdSerie = charac.IdSerie;

                string personajeJson = JsonConvert.SerializeObject(personaje);

                StringContent content = new StringContent(personajeJson, Encoding.UTF8,"application/json");

                await client.PostAsync(request, content);
            }
        }
        #endregion
    }
}
