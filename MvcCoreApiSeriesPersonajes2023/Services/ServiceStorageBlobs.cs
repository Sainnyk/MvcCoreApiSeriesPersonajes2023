using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace MvcCoreApiSeriesPersonajes2023.Services
{
    public class ServiceStorageBlobs
    {
        private BlobContainerClient container;

        public ServiceStorageBlobs(BlobContainerClient container)
        {
            this.container = container;
        }


        //Metodo para subir el blob al servidor. Devolvemos su url
        public async Task<string> UploadBlobAsync(string fileName, Stream stream)
        {
            await this.container.UploadBlobAsync(fileName, stream);
            //Recuperamos la url de nuestro container (el cual contiene una uri absoluta, lo que queremos
            string url = this.container.Uri.AbsoluteUri;
            //Concatenamos la uri (www + container) con el fichero subido
            url = url + "/" + fileName;
            return url;
        }
    }
}
