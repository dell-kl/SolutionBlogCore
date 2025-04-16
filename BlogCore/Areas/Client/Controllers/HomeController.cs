using System.Diagnostics;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public HomeController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel()
            {
                sliders = _unitOfWork.Slider.GetAll(n => n.slider_estado.Equals(true)),
                articulos = _unitOfWork.Articulo.GetAll()
            };

            ViewBag.Inicio = true;

            return View(home);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Articulo articulo = _unitOfWork.Articulo.GetFirstOrDefault(n => n.articulo_id.Equals(id));

            return View(articulo);
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
