using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace e_Commerce.Muebles.WebCliente.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoRepositorio _productoRepositorio;

        // Constructor para inyectar el repositorio
        public ProductosController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        // Acción para mostrar la lista de productos
        public ActionResult Index()
        {
            // Obtener todos los productos
            IEnumerable<Producto> productos = _productoRepositorio.GetAllProductos();
            return View(productos);
        }
    }
}
