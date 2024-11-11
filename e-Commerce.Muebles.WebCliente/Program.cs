using e_Commerce.Muebles.Repos;
using e_Commerce.Muebles.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICarritoRepository>(_ => new CarritoRepository(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IProductoRepositorio>(_ => new ProductoRepos(builder.Configuration["Db:ConnectionString"]));

builder.Services.AddScoped<CarritoService>();

var app = builder.Build();

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
