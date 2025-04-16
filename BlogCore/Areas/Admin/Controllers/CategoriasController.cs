using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CategoriasController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if ( ModelState.IsValid )
            {
                _unitofWork.Categoria.Add(categoria);
                _unitofWork.Save();
                _unitofWork.Dispose();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = _unitofWork.Categoria.GetFirstOrDefault(
                n => n.categoria_id.Equals(id)    
            );

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid) {
                _unitofWork.Categoria.Update(categoria);
                _unitofWork.Save();
                _unitofWork.Dispose();

                return RedirectToAction("Index");
            }

            return View();
        }

        #region -- llamadas a las API --
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Categoria> registros = _unitofWork.Categoria.GetAll();
            return Json(new { data = registros  });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitofWork.Categoria.Remove(id);
                _unitofWork.Save();
                _unitofWork.Dispose();

                return StatusCode(200, new { data = "eliminado exitosmante" });
            }
            catch ( Exception e )
            {
                return StatusCode(404, new { data = "No se puedo eliminar la categoria" });
            }
        }
        #endregion
    }
}
