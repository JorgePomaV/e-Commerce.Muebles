using e_Commerce.Muebles.Repos;
using e_Commerce.Muebles.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios
builder.Services.AddControllersWithViews();

// Registro de repositorios con la cadena de conexión
builder.Services.AddScoped<ICarritoRepository>(_ => new CarritoRepository(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IProductoRepositorio>(_ => new ProductoRepos(builder.Configuration["Db:ConnectionString"]));

// Registro de CarritoService
builder.Services.AddScoped<CarritoService>();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
