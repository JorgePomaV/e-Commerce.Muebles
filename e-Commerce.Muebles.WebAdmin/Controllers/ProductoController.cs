using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.WebAdmin.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using e_Commerce.Muebles.Repos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_Commerce.Muebles.WebAdmin.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductoController(IProductoRepositorio productoRepositorio, ICategoriaRepository categoriaRepository )
        {
            _productoRepositorio = productoRepositorio;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            var productos = _productoRepositorio.GetAllProductos();

            var productoModels = productos.Select(p => new ProductoModel
            {
                id_producto = p.id_producto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                precio = p.precio,
                stock = p.stock,
                categoria_id = p.categoria_id
            }).ToList();

            return View(productoModels);
        }

        public IActionResult Details(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }

            var productoModel = new ProductoModel
            {
                id_producto = producto.id_producto,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                stock = producto.stock,
                categoria_id = producto.categoria_id
            };

            return View(productoModel);
        }

        public IActionResult Create()
        {
            ProductoModel productoModel = new ProductoModel();
            productoModel.ListaCategoriasItem = new List<SelectListItem>();
            var categorias = _categoriaRepository.GetCategorias();
            
            foreach ( var categoria in categorias)
            {
                productoModel.ListaCategoriasItem.Add(new SelectListItem { Value = categoria.id_categoria.ToString(), Text = categoria.categoria });
            }

            return View(productoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoModel productoModel)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    id_producto = productoModel.id_producto,
                    nombre = productoModel.nombre,
                    descripcion = productoModel.descripcion,
                    precio = productoModel.precio,
                    stock = productoModel.stock,
                    categoria_id = productoModel.categoria_id
                };

                _productoRepositorio.AddProducto(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(productoModel);
        }

        public IActionResult Edit(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }

            var productoModel = new ProductoModel
            {
                id_producto = producto.id_producto,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                stock = producto.stock,
                categoria_id = producto.categoria_id
            };

            return View(productoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductoModel productoModel)
        {
            if (id != productoModel.id_producto)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    id_producto = productoModel.id_producto,
                    nombre = productoModel.nombre,
                    descripcion = productoModel.descripcion,
                    precio = productoModel.precio,
                    stock = productoModel.stock,
                    categoria_id = productoModel.categoria_id
                };

                _productoRepositorio.UpdateProducto(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(productoModel);
        }

        public IActionResult Delete(int id)
        {
            var producto = _productoRepositorio.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }

            var productoModel = new ProductoModel
            {
                id_producto = producto.id_producto,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                stock = producto.stock,
                categoria_id = producto.categoria_id
            };

            return View(productoModel);
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
