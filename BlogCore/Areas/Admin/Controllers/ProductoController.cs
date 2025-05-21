using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Utilidades;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string RUTA = "\\pictures\\producto\\";
        private const string RUTA_VIDEO = "\\videos\\producto\\";
        private const string RUTA_IMG_VIDEO = "\\screenshot_videos_producto\\";

        Action<ProductoViewModel, ModelStateDictionary> verificarArchivos = (datos, ModelState) => { 
            Predicate<IFormFileCollection> archivos = (archivo) => archivo.IsNullOrEmpty();

            if ( archivos(datos.filesName) )
                ModelState.AddModelError("filesName", "Debes seleccionar al menos una imagen");
        
            if ( archivos(datos.videosName) )
                ModelState.AddModelError("videosName", "Debes seleccionar al menos un video");
        };

        Func<IUnitofWork, IEnumerable<SelectListItem>> retornarCategorias = (_unitOfWork) => {
            return _unitOfWork.CategoriaProducto.GetAll()
            .Select(n => new SelectListItem()
            {
                Value = n.categoriaProducto_id.ToString(),
                Text = n.categoriaProducto_nombre
            })
            .ToList();
        }; 

        public ProductoController(
            IUnitofWork unitOfWork,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["categorias"] = retornarCategorias(_unitOfWork);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoViewModel modelo)
        {
            if ( ModelState.IsValid )
            {
                if ( !modelo.filesName.IsNullOrEmpty() && !modelo.videosName.IsNullOrEmpty() )
                {
                    _unitOfWork.Producto.Add(modelo.producto);
                    _unitOfWork.Save();

                    //obtenemos el producto especificando todos sus campos. 
                    var producto = _unitOfWork.Producto.GetAll(
                            n => n.producto_nombre.Equals(modelo.producto.producto_nombre) &&
                            n.producto_precio.Equals(modelo.producto.producto_precio) &&
                            n.producto_stock.Equals(modelo.producto.producto_stock) &&
                            n.producto_disponiblidad.Equals(modelo.producto.producto_disponiblidad) &&
                            n.producto_descripcion.Equals(modelo.producto.producto_descripcion) &&
                            n.producto_fechaCreacion.Equals(modelo.producto.producto_fechaCreacion) &&
                            n.producto_fechaModificacion.Equals(modelo.producto.producto_fechaModificacion) &&
                            n.producto_Esdescuento.Equals(modelo.producto.producto_Esdescuento) &&
                            n.producto_descuento.Equals(modelo.producto.producto_descuento)
                        ).FirstOrDefault();

                    //guardamos las imagenes
                    foreach (var item in modelo.filesName)
                    {
                        string ruta = ImagenesUtility.guardarImagen(RUTA, _webHostEnvironment.WebRootPath, item);

                        //guardar la imagen respecitva
                        _unitOfWork.ImagenesProducto.Add(new ImagenesProducto()
                        {
                            imagenesProducto_ruta = ruta,
                            imagenesProducto_estado = true,
                            imagenesProducto_fechaCreacion = DateTime.Now,
                            imagenesProducto_fechaModificacion = DateTime.Now,
                            Productoproducto_id = producto!.producto_id
                        });
                        _unitOfWork.Save();
                    }

                    //guardamos los videos
                    foreach (var item in modelo.videosName)
                    {
                        Dictionary<string, string> ruta = await VideosUtility.guardarVideo(RUTA_IMG_VIDEO, RUTA_VIDEO, _webHostEnvironment.WebRootPath, item);

                        //guardar el video respectivo
                        _unitOfWork.Video.Add(new VideosProducto()
                        {
                            videosProducto_ruta = ruta["rutaVideo"],
                            videosProducto_estado = true,
                            videosProducto_fechaCreacion = DateTime.Now,
                            videosProducto_fechaModificacion = DateTime.Now,
                            videosProducto_rutaFotoVideo = ruta["rutaScreenshot"],
                            Productoproducto_id = producto!.producto_id
                        });
                        _unitOfWork.Save();
                    }

                }
            }

            verificarArchivos(modelo, ModelState);
            ViewData["categorias"] = retornarCategorias(_unitOfWork);

            _unitOfWork.Dispose();

            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Producto productoRegistrado = _unitOfWork.Producto
                .GetFirstOrDefault(n => n.producto_id.Equals(id), includeProperties : "imagenesProducto, videosProducto");
            ViewData["categorias"] = retornarCategorias(_unitOfWork);
            _unitOfWork.Dispose();
            return View(productoRegistrado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductoViewModel modelo)
        {
            //validacion adicional para verificar si el Id mandado es el correcto
            Producto productoRegistrado = _unitOfWork.Producto.GetFirstOrDefault(m => m.producto_id.Equals(modelo.producto.producto_id));
            
            if ( ModelState.IsValid )
            {

                if ( productoRegistrado is not null )
                {
                    if ( !modelo.filesName.IsNullOrEmpty() )
                    {
                        //guardamos las imagenes.
                        foreach (var item in modelo.filesName)
                        {
                            string ruta = ImagenesUtility.guardarImagen(RUTA, _webHostEnvironment.WebRootPath, item);

                            //guardar la imagen respecitva
                            _unitOfWork.ImagenesProducto.Add(new ImagenesProducto()
                            {
                                imagenesProducto_ruta = ruta,
                                imagenesProducto_estado = true,
                                imagenesProducto_fechaCreacion = DateTime.Now,
                                imagenesProducto_fechaModificacion = DateTime.Now,
                                Productoproducto_id = productoRegistrado.producto_id
                            });
                            _unitOfWork.Save();
                        }
                    }

                    if ( !modelo.videosName.IsNullOrEmpty() )
                    {
                        //guardamos los videos
                        foreach (var item in modelo.videosName)
                        {
                            Dictionary<string, string> ruta = await VideosUtility.guardarVideo(RUTA_IMG_VIDEO, RUTA_VIDEO, _webHostEnvironment.WebRootPath, item);

                            //guardar el video respectivo
                            _unitOfWork.Video.Add(new VideosProducto()
                            {
                                videosProducto_ruta = ruta["rutaVideo"],
                                videosProducto_rutaFotoVideo = ruta["rutaScreenshot"],
                                videosProducto_estado = true,
                                videosProducto_fechaCreacion = DateTime.Now,
                                videosProducto_fechaModificacion = DateTime.Now,
                                Productoproducto_id = productoRegistrado.producto_id
                            });
                            _unitOfWork.Save();
                        }
                    }

                    _unitOfWork.Producto.Update(modelo.producto);
                    _unitOfWork.Save();

                }
            }

            //verificarArchivos(modelo, ModelState);
            ViewData["categorias"] = retornarCategorias(_unitOfWork);
            Producto ProductoBuscado = _unitOfWork.Producto
            .GetFirstOrDefault(n => n.producto_id.Equals(modelo.producto.producto_id), includeProperties: "imagenesProducto, videosProducto");
            _unitOfWork.Dispose();

            return View(ProductoBuscado);
        }

        #region -- Api Productos --
        [HttpGet]
        public IActionResult GetProductos(string tipo = "")
        {
            IEnumerable<Producto> productos = new List<Producto>();

            if (tipo.Equals("parcial"))
                productos = _unitOfWork.Producto.GetAll();
            else
                productos = _unitOfWork.Producto.GetAll(
                    includeProperties: "opinionesProducto, imagenesProducto, videosProducto"
                );

            return Json(new { data = productos });
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                Producto productoRegistrado = _unitOfWork.Producto.Get(id);
            
                if ( productoRegistrado is null )
                    return StatusCode(404, new { data = "No se encontro el producto" });

                _unitOfWork.Producto.Remove(productoRegistrado);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                return StatusCode(200, new { data = "Producto eliminado correctamente" });
            }
            catch ( Exception e )
            {
                return StatusCode(500, new { data = "Hay un error en el servidor, contacta con TI" });
            }
        }

        #endregion
    }
}
