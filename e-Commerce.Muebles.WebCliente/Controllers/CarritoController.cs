using e_Commerce.Muebles.ModelFactories;
using e_Commerce.Muebles.Repos;
using e_Commerce.Muebles.Services;
using Microsoft.AspNetCore.Mvc;

public class CarritoController : Controller
{
    private readonly ICarritoRepository _ICarritoRepository;

    public CarritoController(ICarritoRepository carritoService)
    {
        _ICarritoRepository = carritoService;
    }

    public IActionResult Carrito()
    {
        // Lógica para obtener los productos del carrito
        IEnumerable<CarritoCompleto> carrito = _ICarritoRepository.GetCarritosCompleto(ObtenerClienteId());
        var cantidadTotal = carrito.Sum(item => item.cantidad);

        ViewBag.CantidadTotal = cantidadTotal;
        ViewBag.DetallesCarrito = carrito;

        return View(carrito); // Nombre de la vista
    }

    private int ObtenerClienteId()
    {
        var identity = HttpContext.User.Identity;
        var idUsuario = HttpContext.User.Claims.First(x => x.Type == "ClienteEcommerce").Value;
        return int.Parse(idUsuario);
    }

    [HttpPost]
    public IActionResult AgregarProductoAlCarrito(int productoId, int cantidad)
    {
        int clienteId = ObtenerClienteId();

        bool resultado = _ICarritoRepository.RestarCantidadProducto(productoId, cantidad);

        if (!resultado)
        {
            TempData["Error"] = "No hay suficiente inventario para agregar al carrito.";
            return RedirectToAction("Index", "Productos");
        }

        bool resultadoArgegar = _ICarritoRepository.AgregarProductoAlCarrito(clienteId,productoId, cantidad);

        return RedirectToAction("Carrito");
    }

}