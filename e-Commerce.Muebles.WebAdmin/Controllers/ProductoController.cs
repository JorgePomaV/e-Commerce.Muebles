using e_Commerce.Muebles.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using e_Commerce.Muebles.Repos;

namespace e_Commerce.Muebles.WebAdmin.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductosController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public IActionResult Index()
        {
            var productos = _productoRepositorio.GetAllProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepositorio.AddProducto(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        public IActionResult Edit(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.id_producto)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _productoRepositorio.UpdateProducto(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        public IActionResult Delete(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productoRepositorio.DeleteProducto(id);
            return RedirectToAction(nameof(Index));
        }
    }
}