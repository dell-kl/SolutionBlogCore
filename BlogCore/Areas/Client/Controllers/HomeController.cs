using System.Diagnostics;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        public IActionResult Index(string busqueda, string tipo, int indice = 0, int rango = 6 )
        {
            IEnumerable<Articulo> listadoArticulos = (busqueda.IsNullOrEmpty())
                ? _unitOfWork.Articulo.GetAll()
                : _unitOfWork.Articulo.GetAll(
                    n => n.articulo_nombre.Contains(busqueda));
            
            if ( !tipo.IsNullOrEmpty() && tipo.Equals("siguiente") )
            {
                rango += 6;
                if((indice+6) < listadoArticulos.Count()) indice += 6;
                if ( rango > listadoArticulos.Count() ) rango = listadoArticulos.Count();
            }
            else if ( !tipo.IsNullOrEmpty() && tipo.Equals("atras") )
            {
                if (rango.Equals(listadoArticulos.Count()))
                    rango = indice;
                else if ( !rango.Equals(listadoArticulos.Count()) && rango > 6)
                    rango -= 6;

                if(indice > 0) indice -= 6;
            }

            var listadoFiltrado = listadoArticulos.Take(new Range(indice, rango)).ToList();
             
            HomeViewModel home = new HomeViewModel()
            {
                sliders = _unitOfWork.Slider.GetAll(n => n.slider_estado.Equals(true)),
                articulos = listadoFiltrado
            };

            ViewBag.Inicio = true;

            //variables importantes para la parte del paginador
            ViewBag.Indice = indice;
            ViewBag.Rango = rango;
            ViewBag.Busqueda = busqueda;

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
