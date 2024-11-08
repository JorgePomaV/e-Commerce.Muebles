using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Services;
using e_Commerce.Muebles.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dapper;

namespace e_Commerce.Muebles.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _carritoService;
        private readonly IProductoRepositorio _productoRepo;

        public CarritoController(CarritoService carritoService, IProductoRepositorio productoRepos)
        {
            _carritoService = carritoService;
            _productoRepo = productoRepos;
        }

        public IActionResult Index()
        {
            // Obtiene el id del cliente logueado
            int clienteId = ObtenerClienteId();
            var carritoItems = _carritoService.ObtenerCarritoDeCliente(clienteId);
            return View(carritoItems);
        }

        public IActionResult Productos()
        {
            // Llama al servicio de productos para obtener la lista de productos
            var productos = _productoRepo.GetAllProductos();
            return View(productos);
        }

        [HttpPost]
        public IActionResult AgregarProductoAlCarrito(int productoId, int cantidad)
        {
            int clienteId = ObtenerClienteId();
            _carritoService.AgregarProductoAlCarrito(clienteId, productoId, cantidad);
            return RedirectToAction("Index");
        }

        private int ObtenerClienteId()
        {
            return 1;
        }
    }
}