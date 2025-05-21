using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasProductoController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public CategoriasProductoController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public IActionResult Create(CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoriaProducto.Add(categoriaProducto);
                _unitOfWork.Save();

                return RedirectToAction("Create");
            }

            _unitOfWork.Dispose();

            return View();
        }

        [HttpGet]
        public IActionResult Update(Guid guid)
        {
            CategoriaProducto categoriaProducto = _unitOfWork.CategoriaProducto.GetFirstOrDefault(n => n.categoriaProducto_guid.Equals(guid));

            return View(categoriaProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoriaProducto categoriaProducto)
        {

            if ( ModelState.IsValid )
            {
                _unitOfWork.CategoriaProducto.Update(categoriaProducto);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                return RedirectToAction("Index");
            }

            return View();
        }



        #region -- Api datos --

        [HttpGet]
        public IActionResult obtenerCategoriasProducto()
        {
            IEnumerable<CategoriaProducto> listadoCategoriaProducto = _unitOfWork.CategoriaProducto.GetAll();
            return Json(new { Data = listadoCategoriaProducto });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            try
            {   
                int codigo = 200;
                string mensaje = "Se ha eliminado el registro";
                CategoriaProducto categoriaProducto = _unitOfWork.CategoriaProducto.GetFirstOrDefault(n => n.categoriaProducto_guid.Equals(guid));

                if (categoriaProducto != null)
                { 
                    _unitOfWork.CategoriaProducto.Remove(categoriaProducto);
                    _unitOfWork.Save();
                }
                else
                {
                    codigo = 404;
                    mensaje = "No se pudo encontrar el registro";
                }

                _unitOfWork.Dispose();
                return StatusCode(codigo, new { data = mensaje });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { data = "Error en el registro" });
            }
        }
       
        #endregion
    }
}
