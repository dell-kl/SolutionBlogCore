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

        // pulir este delegado para mas adelante. 
        Func<IUnitofWork, int, ComentarioProductoViewModel?, ComentarioProductoViewModel> DatosProductoYComentario = (_unitOfWork, id, modelo) => {
            Producto producto = _unitOfWork.Producto.GetFirstOrDefault(n => n.producto_id.Equals(id), includeProperties: "imagenesProducto, videosProducto");
            IEnumerable<ComentarioProducto> listadoComentarioProducto = _unitOfWork.ComentarioProducto.GetAll(n => n.Productoproducto_id.Equals(id));
            IEnumerable<OpinionesProducto> listadoOpinionesProducto = _unitOfWork.OpinionesProducto.GetAll(n => n.Productoproducto_id.Equals(id));

            if (modelo is null)
                modelo = new ComentarioProductoViewModel()
                {
                    producto = producto,
                    ListadoComentarioProducto = listadoComentarioProducto,
                    ListadoOpinionesProducto = listadoOpinionesProducto
                };
            else
            {
                modelo.producto = producto;
                modelo.ListadoOpinionesProducto = listadoOpinionesProducto;
                modelo.ListadoComentarioProducto = listadoComentarioProducto;
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
        public IActionResult DetailsProduct(int id) =>
            View(DatosProductoYComentario(_unitOfWork, id, null));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsProduct(ComentarioProductoViewModel modelo)
        {
            if ( ModelState.IsValid )
            {
                if (!((ClaimsIdentity)this.User.Identity!).IsAuthenticated)
                    TempData["error"] = "Inicia sesion para comentar o registrate primero";
                else
                {
                    var nombreUsuario = ((ClaimsIdentity)this.User.Identity).Claims.ToList()[1].Value;

                    _unitOfWork.ComentarioProducto.Add(new ComentarioProducto()
                    {
                        comentarioProducto_guid = Guid.NewGuid(),
                        comentarioProducto_nombrePublicador = nombreUsuario,
                        comentarioProducto_descripcion = modelo.comentarioProducto,
                        comentarioProducto_fechaCreacion = DateTime.Now,
                        comentarioProducto_fechaModificacion = DateTime.Now,
                        Productoproducto_id = modelo.producto.producto_id
                    });

                    TempData["succes"] = "Tu comentario permitira que mejoremos nuestra calida de servicio como tienda";

                    _unitOfWork.Save();
                }
            }

            ModelValidationState ValiCampoComent = ModelState
                .Where(n => n.Key.Equals("comentarioProducto")).First().Value!.ValidationState;

            if (ValiCampoComent.ToString().Equals("Invalid")) 
                return View(View(DatosProductoYComentario(_unitOfWork, modelo.producto.producto_id, modelo)));
            
            _unitOfWork.Dispose();

            return RedirectToAction("DetailsProduct", new { id = modelo.producto.producto_id });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AportarComentarioProducto(int identificador_producto_aporte, Guid identificador_mensaje_aporte, string mensaje_aporte)
        {
            var confirmar = identificador_mensaje_aporte.ToString();

            ComentarioProducto? comentarioProducto = _unitOfWork.ComentarioProducto.GetFirstOrDefault(cmt => cmt.comentarioProducto_guid.Equals(identificador_mensaje_aporte));

            if (!((ClaimsIdentity)this.User.Identity!).IsAuthenticated)
                TempData["error"] = "Inicia sesion para comentar o registrate primero";

            else if (
                identificador_producto_aporte != 0
                &&
                comentarioProducto is not null
                &&
                !mensaje_aporte.IsNullOrEmpty()
            )
            {
                // todos los datos se cumplen realizamos el aporte al comentario.
                var nombreUsuario = ((ClaimsIdentity)this.User.Identity).Claims.ToList()[1].Value;

                var mensajeAportar = new ComentarioProducto()
                {
                    comentarioProducto_guid = Guid.NewGuid(),
                    comentarioProducto_nombrePublicador = nombreUsuario,
                    comentarioProducto_descripcion = mensaje_aporte,
                    comentarioProducto_fechaCreacion = DateTime.Now,
                    comentarioProducto_fechaModificacion = DateTime.Now,
                    Productoproducto_id = identificador_producto_aporte,
                    ComentarioProductocomentarioProducto_id = comentarioProducto.comentarioProducto_id
                };

                _unitOfWork.ComentarioProducto.Add(mensajeAportar);
                _unitOfWork.Save();
            }
            else
                TempData["error_comentario_aporte"] = "No se guardo tu comentario, intentalo en otro momento.";

          
            _unitOfWork.Dispose();

            return RedirectToAction("DetailsProduct", new { id = identificador_producto_aporte });
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
