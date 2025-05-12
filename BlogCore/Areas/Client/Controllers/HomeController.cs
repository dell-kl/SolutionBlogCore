using System.Diagnostics;
using System.Security.Claims;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        /*metodos Delegados comnues que se utilizaran*/
        Func<IUnitofWork, int, ComentarioArticuloViewModel?, ComentarioArticuloViewModel> DatosArticuloYComentario = (_unitOfWork, id, modelo) => {
            Articulo articulo = _unitOfWork.Articulo.GetFirstOrDefault(n => n.articulo_id.Equals(id));
            IEnumerable<ComentarioArticulo> listadocomentarioArticulo = _unitOfWork.ComentarioArticulo.GetAll(n => n.Articuloarticulo_id.Equals(id));

            if ( modelo is null )
                modelo = new ComentarioArticuloViewModel()
                {
                    articulo = articulo,
                    listadoComentariosArticulo = listadocomentarioArticulo
                };
            else
            {
                modelo.articulo = articulo;
                modelo.listadoComentariosArticulo = listadocomentarioArticulo;
            }

            return modelo;
        };

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                sliders = _unitOfWork.Slider.GetAll()
            };

            ViewBag.Inicio = true;
            return View(homeViewModel);
        }

        [HttpGet]
        public IActionResult DetailsProduct(int id)
        {
            ComentarioProductoViewModel cmtProductVM = new ComentarioProductoViewModel() { 
                producto = _unitOfWork.Producto.GetFirstOrDefault(p => p.producto_id.Equals(id), includeProperties: "imagenesProducto, videosProducto"),
                ListadoOpinionesProducto = _unitOfWork.OpinionesProducto.GetAll(art => art.opinionesProducto_id.Equals(id))
            };

            return View(cmtProductVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(DatosArticuloYComentario(_unitOfWork, id, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ComentarioArticuloViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                //primero identificar que el usuario se encuentra autenticado.
             
                if ( !((ClaimsIdentity)this.User.Identity!).IsAuthenticated )
                    TempData["error"] = "Inicia sesion para comentar o registrate primero";
                else
                {
                    var nombreUsuario=((ClaimsIdentity)this.User.Identity).Claims.ToList()[1].Value;

                    //guardar comentario
                    _unitOfWork.ComentarioArticulo.Add(new ComentarioArticulo()
                    {
                        comentarioArticulo_guid = Guid.NewGuid(),
                        comentarioArticulo_nombrePublicador = nombreUsuario,
                        comentarioArticulo_descripcion = modelo.comentarioArticulo,
                        Articuloarticulo_id = modelo.articulo.articulo_id,
                        comentarioArticulo_fechaCreacion = DateTime.Now,
                        comentarioArticulo_fechaModificacion = DateTime.Now
                    });

                    TempData["succes"] = "Gracias por dejar tu comentario lo apreciamos mucho";

                    _unitOfWork.Save();
                }

            }

            ModelValidationState ValiCampoComent = ModelState
                .Where(n => n.Key.Equals("comentarioArticulo")).First().Value!.ValidationState;

            if ( ValiCampoComent.ToString().Equals("Invalid"))
                return View(DatosArticuloYComentario(_unitOfWork, modelo.articulo.articulo_id, modelo));
            
            _unitOfWork.Dispose();

            return RedirectToAction("Details", new { id = modelo.articulo.articulo_id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AportarComentario(int identificador_articulo_aporte, Guid identificador_mensaje_aporte, string mensaje_aporte)
        {
            ComentarioArticulo? comentarioArticuloRegistrado = _unitOfWork.ComentarioArticulo.GetFirstOrDefault(n => n.comentarioArticulo_guid.Equals(identificador_mensaje_aporte));

            if (!((ClaimsIdentity)this.User.Identity!).IsAuthenticated)
                TempData["error"] = "Inicia sesion para comentar o registrate primero";
            else if (
                identificador_articulo_aporte != 0 &&
                comentarioArticuloRegistrado != null &&
                !mensaje_aporte.IsNullOrEmpty()
            )
            {
                var nombreUsuario = ((ClaimsIdentity)this.User.Identity).Claims.ToList()[1].Value;

                var mensajeAportar = new ComentarioArticulo()
                {
                    comentarioArticulo_guid = Guid.NewGuid(),
                    comentarioArticulo_nombrePublicador = nombreUsuario,
                    comentarioArticulo_descripcion = mensaje_aporte,
                    Articuloarticulo_id = identificador_articulo_aporte,
                    ComentarioArticulocomentarioArticulo_id = comentarioArticuloRegistrado.comentarioArticulo_id,
                    comentarioArticulo_fechaCreacion = DateTime.Now,
                    comentarioArticulo_fechaModificacion = DateTime.Now
                };

                _unitOfWork.ComentarioArticulo.Add(mensajeAportar);
                _unitOfWork.Save();
            }
            else
                TempData["error_comentario_aporte"] = "No se guardo tu comentario, intentalo en otro momento.";


            ComentarioArticuloViewModel datosVista = DatosArticuloYComentario(_unitOfWork, identificador_articulo_aporte, null);
            _unitOfWork.Dispose();
            return View("Details", datosVista);
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



        #region -- consulta APi -- 

        [HttpGet]
        public IActionResult ConsultarArticulos(int cantidad = 0)
        {
            IEnumerable<Articulo> articulosRegistrados = _unitOfWork.Articulo.GetAll();
            return Json(new { data = articulosRegistrados });
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
           IEnumerable<Producto> productosRegistrados =  _unitOfWork.Producto
                .GetAll(includeProperties: "imagenesProducto");

            return Json(new { data = productosRegistrados });
        }
        #endregion
    }
}
