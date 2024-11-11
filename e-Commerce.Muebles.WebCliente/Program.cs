using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Repos;
using e_Commerce.Muebles.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
   .AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, opciones =>
{
    opciones.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;// + ".apps.googleusercontent.com";
    opciones.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientPriv").Value;

    // Evento que maneja el fallo de autenticación
    opciones.Events.OnRemoteFailure = context =>
    {
        // Si el usuario deniega el acceso, redirigirlo a una página de acceso denegado
        if (context.Failure is AuthenticationException)
        {
            //falta implementar pagina de acceso denegado
            context.Response.Redirect("/Home/AccessDenied"); // Página de acceso denegado
        }
        else
        {
            // Propaga el error en otros casos
            context.HandleResponse(); // Deja que el sistema maneje el error normalmente
        }

        return Task.CompletedTask;
    };

    opciones.Events.OnCreatingTicket = ctx =>
    {
        var usuarioServicio = ctx.HttpContext.RequestServices.GetRequiredService<IUserRepositorio>();

        string googleNameIdentifier = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString(); ;

        var usuario = usuarioServicio.GetUsuarioPorGoogleSubject(googleNameIdentifier);
        int idUsuario = 0;
        if (usuario == null)
        {
            Autenticacion autenticacionNuevo = new Autenticacion();
            Usuario usuarioNuevo = new Usuario();
            usuarioNuevo.Apellido = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value.ToString();
            usuarioNuevo.Nombre = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value.ToString();
            //usuarioNuevo.NombreCompleto = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value.ToString();
            autenticacionNuevo.GoogleIdentificador = googleNameIdentifier;
            // usuarioNuevo.Borrado = false;
            autenticacionNuevo.Email = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
            //usuarioNuevo.IdUsuarioAlta = 1;

            usuarioNuevo.tipo_usuario = "Cliente";
            idUsuario = usuarioServicio.AddUsuario(usuarioNuevo, autenticacionNuevo);

        }
        else
        {
            idUsuario = usuario.id_usuario;
        }
        //ctx.Identity.
        //   usuarioServicio.GetUsuarioPorGoogleSubject(ctx.Identity.Claims)
        // Agregar reclamaciones personalizadas aquí
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("ClienteEcommerce", idUsuario.ToString()));

        ctx.Identity.AddClaim(new System.Security.Claims.Claim("UNLZRole", "Cliente"));

        var accessToken = ctx.AccessToken;
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("accessToken", accessToken));

        return Task.CompletedTask;
    };
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICarritoRepository>(_ => new CarritoRepository(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IProductoRepositorio>(_ => new ProductoRepos(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IUserRepositorio>(_ => new UserRepos(builder.Configuration["Db:ConnectionString"]));

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
