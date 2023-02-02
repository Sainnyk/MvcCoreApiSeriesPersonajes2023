using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreApiSeriesPersonajes2023.Models
{
    public class Personaje
    {

            public int IdPersonaje { get; set; }

            public String Nombre { get; set; }

            public string Imagen { get; set; }

            public int IdSerie { get; set; }
        
    }
}
