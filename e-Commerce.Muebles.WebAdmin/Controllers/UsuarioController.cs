using e_Commerce.Muebles.Entidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using e_Commerce.Muebles.Repos;

namespace e_Commerce.Muebles.WebAdmin.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;

        public UsuariosController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
        }

        public ActionResult Index()
        {
            var usuarios = _userRepositorio.GetAllUsuarios();
            return View(usuarios);
        }

        public ActionResult Details(int id)
        {
            var usuario = _userRepositorio.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepositorio.AddUsuario(usuario);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message);
            }
            return View(usuario);
        } 

        public ActionResult Edit(int id)
        {
            var usuario = _userRepositorio.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepositorio.UpdateUsuario(usuario);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el usuario: " + ex.Message);
            }
            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            var usuario = _userRepositorio.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _userRepositorio.DeleteUsuario(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el usuario: " + ex.Message);
            }
            return View();
        }
    }
}