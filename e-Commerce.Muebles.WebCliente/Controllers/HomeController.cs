using e_Commerce.Muebles.WebCliente.Models;
using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Collections.Generic;

namespace e_Commerce.Muebles.WebCliente.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoRepositorio _productoRepos; 

        public HomeController(ILogger<HomeController> logger, IProductoRepositorio productoService)
        {
            _logger = logger;
            _productoRepos = productoService;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> productos = _productoRepos.GetAllProductos();

            return View(productos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
