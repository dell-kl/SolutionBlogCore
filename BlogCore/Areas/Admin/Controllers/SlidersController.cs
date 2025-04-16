using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string RUTA = "\\pictures\\slider\\";
        public SlidersController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        #region -- metodos y verificacion --
        Predicate<IFormFileCollection> vacioImagen = (imagen) => imagen.IsNullOrEmpty();
        Action<string> eliminarIMG = ImagenesUtility.eliminarImagen;
        Func<string, string, IFormFile, string> guardarIMG = ImagenesUtility.guardarImagen;
        #endregion


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
        public IActionResult Create(SliderViewModel modelo)
        {

            if ( ModelState.IsValid )
            {
                if ( !vacioImagen(HttpContext.Request.Form.Files) )
                {
                    modelo.slider.slider_rutaImagen = guardarIMG(RUTA, _webHostEnvironment.WebRootPath, modelo.fileName);
                   
                    _unitOfWork.Slider.Add(modelo.slider);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    return RedirectToAction("Index");
                }
            }

            if ( vacioImagen(HttpContext.Request.Form.Files) )
                ModelState.AddModelError("fileName", "Debes ingresar una imagen");
            
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slider slider = _unitOfWork.Slider.GetFirstOrDefault(n => n.slider_id.Equals(id));

            if (slider is null)
                return RedirectToAction("Index");

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderViewModel modelo)
        {
            if ( ModelState.IsValid )
            {
                if ( !vacioImagen( HttpContext.Request.Form.Files ) )
                {
                    eliminarIMG($"{_webHostEnvironment.WebRootPath}{modelo.slider.slider_rutaImagen}");
                    modelo.slider.slider_rutaImagen = guardarIMG(RUTA, _webHostEnvironment.WebRootPath, modelo.fileName);
                }

                _unitOfWork.Slider.Update(modelo.slider);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                return RedirectToAction("Index");
            }


            return View(modelo.slider.slider_id);
        }


        #region -- llamadas a API -- 
        [HttpGet]
        public IActionResult getAll() 
        {
            IEnumerable<Slider> sliders = _unitOfWork.Slider.GetAll();
            return Json(new { data = sliders });
        }

        [HttpPut]
        public IActionResult StateChange(int id, bool estado) {



            return View();
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            try
            {
                _unitOfWork.Slider.Remove(id);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                return StatusCode(200, new { data = "Slider eliminado exitosamente" });
            }
            catch ( Exception e )
            {
                return StatusCode(500, new { data = "Error al eliminar el Slider" });
            }
        }
        
        #endregion
    }
}
