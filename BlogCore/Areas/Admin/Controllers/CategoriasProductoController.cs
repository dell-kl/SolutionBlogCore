using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoriaProducto.Add(categoriaProducto);
                _unitOfWork.Save();
            }

            _unitOfWork.Dispose();

            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(CategoriaProducto categoriaProducto)
        {
            return View();
        }

        #region -- Api datos --

        [HttpGet]
        public IActionResult obtenerCategoriasProducto()
        {
            IEnumerable<CategoriaProducto> listadoCategoriaProducto = _unitOfWork.CategoriaProducto.GetAll();
            return Json(new { Data = listadoCategoriaProducto });
        }
        #endregion
    }
}
