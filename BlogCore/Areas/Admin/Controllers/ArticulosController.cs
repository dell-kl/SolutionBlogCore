using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using System.Drawing;
using System.Net;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IUnitofWork _UnitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string RUTA = "\\pictures\\articulo\\";

        public ArticulosController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }

        #region -- metodos y verificacion --
        Predicate<IFormFileCollection> vacioImagen = (imagen) => imagen.IsNullOrEmpty();
        Action<string> eliminarIMG = ImagenesUtility.eliminarImagen;
        Func<string, string, IFormFile, string> guardarIMG = ImagenesUtility.guardarImagen;
        Func<IUnitofWork, IEnumerable<SelectListItem>> lstCatalog = (unitOfWork) => unitOfWork.Articulo.obtenerCategorias();  
        #endregion

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() {
            TempData["categorias"] = _UnitofWork.Articulo.obtenerCategorias();
            return View();      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticulosViewModel modelo) {
            //var contenido = HttpContext.Request.Form.Files; <- se puede validar con esto para verificar si se envio algun archivo.  
            if (ModelState.IsValid) 
            {
                if ( !vacioImagen(HttpContext.Request.Form.Files) )
                {
                    modelo.articulo.articulo_rutaImagen = guardarIMG(RUTA, _webHostEnvironment.WebRootPath, modelo.fileName);

                    _UnitofWork.Articulo.Add(modelo.articulo);
                    _UnitofWork.Save();
                    _UnitofWork.Dispose();

                    return RedirectToAction("Index");
                }
            }

            if ( vacioImagen(HttpContext.Request.Form.Files) )
                ModelState.AddModelError("fileName", "Debes ingresar una imagen");

            TempData["categorias"] = lstCatalog(_UnitofWork);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Articulo articulo = _UnitofWork.Articulo.GetFirstOrDefault(
                n => n.articulo_id.Equals(id)
            );

            if (articulo == null)
                return RedirectToAction("Index");

            TempData["categorias"] = lstCatalog(_UnitofWork);

            return View(articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticulosViewModel modelo)
        {
            if ( ModelState.IsValid )
            {
                if ( !vacioImagen(HttpContext.Request.Form.Files) )
                {
                    eliminarIMG($"{_webHostEnvironment.WebRootPath}{modelo.articulo.articulo_rutaImagen}");
                    modelo.articulo.articulo_rutaImagen = guardarIMG(RUTA, _webHostEnvironment.WebRootPath, modelo.fileName );
                }

                _UnitofWork.Articulo.Update(modelo.articulo);
                _UnitofWork.Save();
                _UnitofWork.Dispose();

                return RedirectToAction("Index");
            }

            TempData["categorias"] = lstCatalog(_UnitofWork);
            return View(modelo.articulo);
        }


        #region -- llamada a API --

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Articulo> articulos = _UnitofWork.Articulo.GetAll(
                includeProperties : "categoria"
            );

            return Json(new { data = articulos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                //tenemos que eliminar la imagen del servidor.
                Articulo articulo = _UnitofWork.Articulo.GetFirstOrDefault(art => art.articulo_id.Equals(id));

                if ( articulo == null )
                    return StatusCode(404, new { data = "Articulo no encontrado" });

                eliminarIMG($"{_webHostEnvironment.WebRootPath}{articulo.articulo_rutaImagen}");

                _UnitofWork.Articulo.Remove(articulo);
                _UnitofWork.Save();
                _UnitofWork.Dispose();

                return StatusCode(200, new { data = "eliminado exitosamante" });
            }
            catch (Exception e)
            {
                return StatusCode(404, new { data = "No se puedo eliminar el articulo" });
            }
        }

        #endregion

    }
}
