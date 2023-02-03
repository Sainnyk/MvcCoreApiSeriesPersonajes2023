namespace MvcCoreApiSeriesPersonajes2023.Helpers
{
    public enum Folders
    {
        Uploads, Images, Documents, Temp
    }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        //Variable para recuperar el host de mi pagina web, donde esté alojada
        private string HostUrl;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.hostEnvironment = hostEnvironment;
            this.HostUrl = httpContextAccessor.HttpContext.Request.Host.Value;
            //this.hostEnvironment.WebRootPath -> Sirve para la ruta en la nube, el problema es que devuelve donde estoy ubicado, pero yo quiero
            //el host, la url principal sin \
        }

        //Tendremos un método que nos devolvera la ruta dependiendo de la carpeta seleccionada
        public string GetMapPath(Folders folder, string filename)
        {
            string carpeta = "";
            if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }
            else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Temp)
            {
                carpeta = "temp";
            }

            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename);

            return path;
        }

        //Sirve para encontrar la RUTA  a mi archivo, no para saber el nombre de la carpeta.
        public string GetNameFolder(Folders folder)
        {

            if (folder == Folders.Uploads)
            {
                return "uploads";
            }
            else if (folder == Folders.Documents)
            {
                return "documents";
            }
            else if (folder == Folders.Images)
            {
                return "images";
            }
            else if (folder == Folders.Temp)
            {
                return "document/temp";
            }
            return "";
        }

        public string GetHostUrl()
        {
            return "https://" + this.HostUrl + "/";
        }
    }
}
