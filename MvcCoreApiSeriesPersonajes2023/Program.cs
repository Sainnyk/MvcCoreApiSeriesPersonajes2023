using Azure.Storage.Blobs;
using MvcCoreApiSeriesPersonajes2023.Helpers;
using MvcCoreApiSeriesPersonajes2023.Services;

var builder = WebApplication.CreateBuilder(args);

//Recuperamos las claves
string azurekey = builder.Configuration.GetConnectionString("azurekey");
string personajerContainer = builder.Configuration.GetValue<string>("AzureContainers:personajescontainer");
//Creamos nuestro cliente para acceder al servicio de Azure Blobs mediante nuestras claves
BlobServiceClient blobServiceClient = new BlobServiceClient(azurekey);
//Creamos nuestro Container Client con el nombre del contenedor
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(personajerContainer);

builder.Services.AddTransient<BlobContainerClient>(x=>containerClient);
//Ponemos nuestro Service Storage Blobs en la aplicacion
builder.Services.AddTransient<ServiceStorageBlobs>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<HelperPathProvider>();

string url = builder.Configuration.GetValue<string>("UrlsApi:ApiSeries");
builder.Services.AddTransient<ServiceSeries>(x => new ServiceSeries(url));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
