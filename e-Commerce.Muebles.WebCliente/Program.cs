using e_Commerce.Muebles.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
void ConfigureServices(IServiceCollection services)
{
    // Configuraci�n del repositorio con la cadena de conexi�n
    services.AddScoped<IProductoRepositorio, ProductoRepos>(provider =>
        new ProductoRepos("tu_cadena_de_conexion_aqui"));

    services.AddControllersWithViews();  // Configuraci�n para habilitar las vistas en MVC
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
