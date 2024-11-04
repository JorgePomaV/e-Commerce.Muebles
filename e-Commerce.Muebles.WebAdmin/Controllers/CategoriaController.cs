using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce.Muebles.WebAdmin.Controllers
{
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _CategoriaRepository;
        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _CategoriaRepository = categoriaRepository;
        }
        // GET: CategoriaController
        public ActionResult Index()
        {
            var categorias = _CategoriaRepository.GetCategorias();
            return View(categorias);
        }

        // GET: CategoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    categoria = collection["categoria"]
                };
                _CategoriaRepository.agregarCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View(_CategoriaRepository.GetCategoria(id));
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    id_categoria = int.Parse(collection["id_categoria"]),
                    categoria = collection["categoria"],
                };
                _CategoriaRepository.editarCategoria(id,categoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
     
            return View(_CategoriaRepository.GetCategoria(id));
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _CategoriaRepository.BorrarCategoria(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
