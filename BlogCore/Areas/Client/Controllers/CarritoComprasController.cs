using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BlogCore.Utilidades;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class CarritoComprasController : Controller
    {
        private readonly IDataSecurityRepository _dataSecurity;
        private readonly IUnitofWork _unitOfWork;

        #region -- Delegados -- 
        Func<HttpRequest, HttpResponse, IDataSecurityRepository, Dictionary<string, string>> configuracionCookies = (Request, Response, _dataSecurity) => {
            Dictionary<string, string> datos = new Dictionary<string, string>();
            bool isCookie = Request.Cookies.ContainsKey(DefinitionKeyCookies.keyShoppingCart);
            // si no hay cookie la crea, caso contrario toma la que ya esta y la desencripta para usarla.
            string IDCarritoCookie = (!isCookie) ? $"ID_SESSION_{Guid.NewGuid().ToString()}" : _dataSecurity.desencriptarDatos(Request.Cookies[DefinitionKeyCookies.keyShoppingCart]!);

            //si no hay cookie nos encargamos de crea las configuraciones para que el navegador
            //la acepte.
            if ( !isCookie )
            {
                string IDCarritoCookieEncriptado = _dataSecurity.encriptarDatos(IDCarritoCookie);

                CookieOptions cookieOptiones = new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/"
                };

                Response.Cookies.Append(DefinitionKeyCookies.keyShoppingCart, IDCarritoCookieEncriptado, cookieOptiones);

                datos.Add("tipo", "COOKIE_NEW");
            }
            else
                datos.Add("tipo", "COOKIE_OLD");
            

            datos.Add("cookie", IDCarritoCookie);

            return datos;
        };
        #endregion

        public CarritoComprasController(IDataSecurityRepository dataSecurity, IUnitofWork unitOfWork)
        {
            _dataSecurity = dataSecurity;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CarritoViewModel carritoViewModel = new CarritoViewModel();
            //hacer persistente la cookie 
            string? cookieCarrito = Request.Cookies[DefinitionKeyCookies.keyShoppingCart];

            if ( !cookieCarrito.IsNullOrEmpty() )
            {

                string CookieTextoPlano = _dataSecurity.desencriptarDatos(cookieCarrito!);
                carritoViewModel.carrito = _unitOfWork.Carrito.GetFirstOrDefault(n => n.carrito_sessionId.Equals(CookieTextoPlano));
                carritoViewModel.carritoCompra = _unitOfWork.CarritoCompra.GetAll(n => n.Carritocarrito_id.Equals(carritoViewModel.carrito.carrito_id));

                ICollection<Producto> productos = new List<Producto>();
                foreach (var item in carritoViewModel.carritoCompra)
                {
                    
                    Producto producto = _unitOfWork.Producto.GetFirstOrDefault(n => n.producto_id.Equals(item.Productoproducto_id), includeProperties: "imagenesProducto,categoriaProducto");
                    productos.Add(producto);
                }
                carritoViewModel.producto = productos;
            }

            return View(carritoViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string registro_aWQgZGVsIHByb2R1Y3RvCg, string cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y)
        {
            //Este verifica si no existe cookie (La crea, la encripta y la manda como respuesta)
            //Si ya hay cookie solamente la desencripta para poder usarla.
            Dictionary<string, string> configuracionCokie = configuracionCookies(Request, Response, _dataSecurity);
            string idProducto = _dataSecurity.desencriptarDatos(registro_aWQgZGVsIHByb2R1Y3RvCg);
            string cantidadProducto = _dataSecurity.desencriptarDatos(cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y);

            Carrito carrito = new Carrito() { carrito_sessionId = configuracionCokie["cookie"] };
            
            //Verifica si hay inicio de sesion (SI HAY LO PONE EN EL ATRIBUTO IdentityUserId de Carrito)
            //Caso contrario permanece null
            var claimsIdentity = ((ClaimsIdentity)this.User.Identity!);
            if (claimsIdentity.IsAuthenticated)
                carrito.IdentityUserId = claimsIdentity.Claims.ToList()[0].Value;

            if (configuracionCokie["tipo"].Equals("COOKIE_NEW"))
            {
                //1.- Creamos el carrito
                _unitOfWork.Carrito.Add(carrito);
                _unitOfWork.Save();

                //2.- Agregamos el producto.
                _unitOfWork.CarritoCompra.Add(new CarritoCompra()
                {
                    Productoproducto_id = int.Parse(idProducto),
                    carritoCompra_cantidad = int.Parse(cantidadProducto),
                    Carritocarrito_id = _unitOfWork.Carrito.GetFirstOrDefault(c => c.carrito_sessionId.Equals(configuracionCokie["cookie"])).carrito_id
                });
                _unitOfWork.Save();
            }

            if (configuracionCokie["tipo"].Equals("COOKIE_OLD") )
            {
                //si es vieja la cookie verificada desde 'configuracionCookie' tenemos que realizar lo siguiente
                Carrito carritoSesionCookie = _unitOfWork.Carrito.GetFirstOrDefault(
                    carrito => carrito.carrito_sessionId.Equals(configuracionCokie["cookie"]),
                    includeProperties: "carritoCompra"
                );

                var productoBuscado = carritoSesionCookie.carritoCompra.Where(n => n.Productoproducto_id.Equals(int.Parse(idProducto)));
                if ( productoBuscado.Count() == 0 )
                {
                    // 1.- Si el Id del producto es nuevo (entonces agregamos)
                    _unitOfWork.CarritoCompra.Add(new CarritoCompra()
                    {
                        Productoproducto_id = int.Parse(idProducto),
                        carritoCompra_cantidad = int.Parse(cantidadProducto),
                        Carritocarrito_id = carritoSesionCookie.carrito_id
                    });
                    _unitOfWork.Save();
                }
                else
                {
                    // 2.- Si el Id del producto ya existe entonces actualizamos CarritoCompra
                    productoBuscado.First().carritoCompra_cantidad += int.Parse(cantidadProducto);
                    _unitOfWork.CarritoCompra.Update(productoBuscado.First());
                    _unitOfWork.Save();
                }
            }

            _unitOfWork.Dispose();            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
            CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid));

            _unitOfWork.CarritoCompra.Remove(registroProducto);
            _unitOfWork.Save();
            _unitOfWork.Dispose();

            return RedirectToAction("Index");
        }

        #region -- endpoints usados para comunicarse con jS -- 
        [HttpGet]
        public IActionResult IncrementAmountProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            try
            {
                Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
                CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid), includeProperties: "producto");
                string mensaje = "No puedes tomar una cantidad mayor al stock";

                if ( registroProducto.carritoCompra_cantidad <= registroProducto.producto.producto_stock)
                {
                    registroProducto.carritoCompra_cantidad += 1;
                    _unitOfWork.CarritoCompra.Update(registroProducto);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();
                    
                    mensaje = "cantidad agregado exitosamente";
                }

                return StatusCode(200, new { data = mensaje });
            }
            catch(Exception error )
            {
                return StatusCode(500, new { data = "Intentalo otra vez" });
            }
        }

        [HttpGet]
        public IActionResult DecrementAmountProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            try
            {
                Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
                CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid));
                string mensaje = "No se pudo realizar dicha eliminacio de cantidad";
                if ( registroProducto.carritoCompra_cantidad > 0 )
                {
                    registroProducto.carritoCompra_cantidad -= 1;
                    _unitOfWork.CarritoCompra.Update(registroProducto);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    mensaje = "cantidad eliminada exitosamente";
                }

                return StatusCode(200, new { data = mensaje });
            }
            catch(Exception error )
            {
                return StatusCode(500, new { data = "Intentalo otra vez" });
            }
        }
        #endregion
    }
}
