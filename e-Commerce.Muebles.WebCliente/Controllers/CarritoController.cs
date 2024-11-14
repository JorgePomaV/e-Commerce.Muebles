using e_Commerce.Muebles.Services;
using Microsoft.AspNetCore.Mvc;

public class CarritoController : Controller
{
    private readonly CarritoService _carritoService;

    public CarritoController(CarritoService carritoService)
    {
        _carritoService = carritoService;
    }

    public IActionResult Carrito()
    {
        // Lógica para obtener los productos del carrito
        var carrito = _carritoService.ObtenerCarritoDeCliente(ObtenerClienteId());
        var cantidadTotal = carrito.Sum(item => item.cantidad);

        ViewBag.CantidadTotal = cantidadTotal;
        ViewBag.DetallesCarrito = carrito;

        return View("Carrito"); // Nombre de la vista
    }

    private int ObtenerClienteId()
    {
        var identity = HttpContext.User.Identity;
        var idUsuario = HttpContext.User.Claims.First(x => x.Type == "ClienteEcommerce").Value;
        return int.Parse(idUsuario);
    }
}