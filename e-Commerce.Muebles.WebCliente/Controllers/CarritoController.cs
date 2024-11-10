using e_Commerce.Muebles.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce.Muebles.WebCliente.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _carritoService;

        public CarritoController(CarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpPost]
        public IActionResult AgregarProductoAlCarrito(int productoId, int cantidad)
        {
            int clienteId = ObtenerClienteId(); 

            bool resultado = _carritoService.AgregarProductoAlCarrito(clienteId, productoId, cantidad);

            if (resultado)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error"); 
            }
        }

        private int ObtenerClienteId()
        {
            return 1; 
        }
    }
}
