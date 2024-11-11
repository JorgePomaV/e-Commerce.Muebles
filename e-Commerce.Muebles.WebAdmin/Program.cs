using e_Commerce.Muebles.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductoRepositorio>(_ => new ProductoRepos(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<ICategoriaRepository>(_ => new CategoriaRepository(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IUserRepositorio>(_ => new UserRepos(builder.Configuration["Db:ConnectionString"]));
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
