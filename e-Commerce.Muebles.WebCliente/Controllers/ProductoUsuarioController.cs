using Microsoft.AspNetCore.Mvc;
using e_Commerce.Muebles.Repos;  // Asegúrate de tener este namespace si tu repositorio está allí
using e_Commerce.Muebles.Entidades;  // Asegúrate de tener este namespace si la entidad Producto está allí

namespace e_Commerce.Muebles.Controllers
{
    public class ProductoUsuarioController : Controller
    {
        private readonly IProductoRepositorio _productoRepositorio;

        // Constructor donde se inyecta la dependencia del repositorio
        public ProductoUsuarioController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        // Acción Index que obtiene todos los productos y los pasa a la vista
        public IActionResult Index()
        {
            // Obtener todos los productos desde el repositorio
            var productos = _productoRepositorio.GetAllProductos();

            // Pasar los productos a la vista
            return View(productos);
        }
    }
}

