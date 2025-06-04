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

        Func<ClaimsIdentity, HttpRequest, IDataSecurityRepository, IUnitofWork, CarritoViewModel> InformacionCarritoCompra = (claimsIdentity, Request, _dataSecurity, _unitOfWork) => {
            CarritoViewModel carritoViewModel = new CarritoViewModel();
            bool auth = claimsIdentity.IsAuthenticated;
            string? cookieCarrito = Request.Cookies[DefinitionKeyCookies.keyShoppingCart];

            if (!cookieCarrito.IsNullOrEmpty())
            {
                // debemos realizar una verificacion de productos almancedos para sesiones de usuario autenticados y no autenticados.
                string CookieTextoPlano = _dataSecurity.desencriptarDatos(cookieCarrito!);
                carritoViewModel.carrito = _unitOfWork.Carrito.GetFirstOrDefault(n => n.carrito_sessionId.Equals(CookieTextoPlano));

                //traer los datos del carrito de compra de clientes autenticados y no autenticados.
                if ( 
                !auth && carritoViewModel.carrito.IdentityUserId is null
                || 
                auth && carritoViewModel.carrito.IdentityUserId is not null && claimsIdentity.Claims.ToList()[0].Value.Equals(carritoViewModel.carrito.IdentityUserId) )
                {
                    carritoViewModel.carritoCompra = _unitOfWork.CarritoCompra.GetAll(n => n.Carritocarrito_id.Equals(carritoViewModel.carrito.carrito_id));

                    ICollection<Producto> productos = new List<Producto>();
                    foreach (var item in carritoViewModel.carritoCompra)
                    {
                        Producto producto = _unitOfWork.Producto.GetFirstOrDefault(n => n.producto_id.Equals(item.Productoproducto_id), includeProperties: "imagenesProducto,categoriaProducto");
                        productos.Add(producto);
                    }
                    carritoViewModel.producto = productos;
                    carritoViewModel.configurarDatosCompra();   
                }

            }

            return carritoViewModel;
        };

        Func<IUnitofWork, int, int, ClaimsIdentity, Dictionary<string, string>, Action> escuchandoCarritoCompra = (_unitOfWork, id_producto, cantidad_producto, claimsIdentity, cookie) =>
        {
            //lo primero que se ejecutara y verificara
            Carrito carrito = (cookie["tipo"].Equals("COOKIE_NEW")) 
                            ? new Carrito() { 
                                carrito_sessionId = cookie["cookie"]
                            }
                            : _unitOfWork.Carrito.GetFirstOrDefault(
                                carrito => carrito.carrito_sessionId.Equals(cookie["cookie"]),
                                includeProperties: "carritoCompra");

            //si el cliente inicio sesion podemos setearle su id de usuario
            string? idUsuario = (carrito.IdentityUserId.IsNullOrEmpty()) 
                ? (claimsIdentity.IsAuthenticated) ? claimsIdentity.Claims.ToList()[0].Value : null : carrito.IdentityUserId;
            carrito.IdentityUserId = idUsuario;

            //la parte de la suma de las cantidad de los precios que vamos generado.
            Producto? producto = _unitOfWork.Producto.GetFirstOrDefault(n => n.producto_id.Equals(id_producto));
            decimal precioGenerado = (producto is not null) ? (
                    ((bool)producto.producto_Esdescuento!) ? (cantidad_producto * producto.producto_precioDescuento) : (cantidad_producto * producto.producto_precio)
                ) : 0.00m;

            if (cookie["tipo"].Equals("COOKIE_NEW"))
            {
                carrito.carrito_precioTotal = precioGenerado;
                _unitOfWork.Carrito.Add(carrito);
            }
            else if (cookie["tipo"].Equals("COOKIE_OLD"))
            {
                carrito.carrito_precioTotal += precioGenerado;
                _unitOfWork.Carrito.Update(carrito);
            }
           
            CarritoCompra carritoCompra = new CarritoCompra()
            {
                Productoproducto_id = id_producto,
                carritoCompra_cantidad = cantidad_producto
            };

            _unitOfWork.Save();

            return () => {

                if (cookie["tipo"].Equals("COOKIE_NEW"))
                {
                    carritoCompra.Carritocarrito_id = _unitOfWork.Carrito
                    .GetFirstOrDefault(c => c.carrito_sessionId.Equals(cookie["cookie"]))
                    .carrito_id;

                    _unitOfWork.CarritoCompra.Add(carritoCompra);
                }
                else
                {
                    CarritoCompra? productoBuscado = carrito.carritoCompra
                        .Where(n => n.Productoproducto_id.Equals(id_producto))
                        .FirstOrDefault();

                    if (productoBuscado is null)
                    {
                        carritoCompra.Carritocarrito_id = carrito.carrito_id;
                        _unitOfWork.CarritoCompra.Add(carritoCompra);
                    }
                    else
                    {
                        productoBuscado.carritoCompra_cantidad += cantidad_producto;
                        _unitOfWork.CarritoCompra.Update(productoBuscado);
                    }
                }

                _unitOfWork.Save();
            };
        };
        #endregion

        public CarritoComprasController(IDataSecurityRepository dataSecurity, IUnitofWork unitOfWork)
        {
            _dataSecurity = dataSecurity;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() => View(InformacionCarritoCompra(((ClaimsIdentity)this.User.Identity!), Request, _dataSecurity, _unitOfWork)); 
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string registro_aWQgZGVsIHByb2R1Y3RvCg, string cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y)
        {
            string idProducto = _dataSecurity.desencriptarDatos(registro_aWQgZGVsIHByb2R1Y3RvCg);
            string cantidadProducto = _dataSecurity.desencriptarDatos(cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y);
            ClaimsIdentity claimsIdentity = ((ClaimsIdentity)this.User.Identity!);
            Action ejecutarAccionCarritoCompra = escuchandoCarritoCompra(_unitOfWork, int.Parse(idProducto), int.Parse(cantidadProducto), claimsIdentity, configuracionCookies(Request, Response, _dataSecurity));
            ejecutarAccionCarritoCompra();
            _unitOfWork.Dispose();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            try
            {
                //tenemos que restar el total que sale de la cantidad*precio_producto al precio_total del carrito para su actualizacion.
                Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
                CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid), includeProperties: "producto, carrito");

                decimal cantidadTotalProductoAEliminar = registroProducto.carritoCompra_cantidad*(
                    (bool)registroProducto.producto.producto_Esdescuento! ? registroProducto.producto.producto_precioDescuento : registroProducto.producto.producto_precio
                );

                Carrito carrito = registroProducto.carrito;
                carrito.carrito_precioTotal -= cantidadTotalProductoAEliminar;

                _unitOfWork.Carrito.Update(carrito);
                _unitOfWork.CarritoCompra.Remove(registroProducto);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                TempData["success"] = "Producto eliminado exitosamente del carrito de compras.";
            }
            catch ( Exception error )
            {
                TempData["error"] = "No se elimino el producto, intentalo otra vez.";
            }

            return RedirectToAction("Index");
        }

        #region -- endpoints usados para comunicarse con jS -- 
        [HttpPost]
        public async Task<IActionResult> IncrementAmountProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            try
            {
                /* Intermedio - Ejecucion de codigo - (Aprovechamos el hilo ejecutado en tro procesador) */
                
                string mensaje = "No puedes tomar una cantidad mayor al stock";
                Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
                CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid), includeProperties: "carrito, producto");

                if (registroProducto.carritoCompra_cantidad <= registroProducto.producto.producto_stock)
                {
                    Carrito carrito = registroProducto.carrito;
                    decimal precioTotalProductoAAgregar = (bool)registroProducto.producto.producto_Esdescuento! ? registroProducto.producto.producto_precioDescuento : registroProducto.producto.producto_precio;

                    carrito.carrito_precioTotal += precioTotalProductoAAgregar;
                    registroProducto.carritoCompra_cantidad += 1;

                    _unitOfWork.Carrito.Update(carrito);
                    _unitOfWork.CarritoCompra.Update(registroProducto);
                    _unitOfWork.Save();
                   
                    mensaje = "cantidad agregado exitosamente";
                }

                /* Intermedio - Ejecucion de codigo - (Aprovechamos el hilo ejecutado en tro procesador) */

                CarritoViewModel carritoViewModel = InformacionCarritoCompra(((ClaimsIdentity)this.User.Identity!), Request, _dataSecurity, _unitOfWork);

                _unitOfWork.Dispose();

                return StatusCode(200, new { 
                    data = mensaje, 
                    precioTotal = carritoViewModel.precioTotal,
                    preciosProductos = carritoViewModel.productosPreciosConfigurados
                });
            }
            catch(Exception error )
            {
                return StatusCode(500, new { data = "Intentalo otra vez" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DecrementAmountProduct(string guid_Z3VpZCBwcm9kdWN0bwo)
        {
            try
            {
                Guid guid = Guid.Parse(_dataSecurity.desencriptarDatos(guid_Z3VpZCBwcm9kdWN0bwo));
                CarritoCompra registroProducto = _unitOfWork.CarritoCompra.GetFirstOrDefault(n => n.carritoCompra_guid.Equals(guid), includeProperties: "carrito");
                string mensaje = "No se pudo realizar dicha eliminacio de cantidad";
                if ( registroProducto.carritoCompra_cantidad > 0 )
                {
                    Carrito carrito = registroProducto.carrito;
                    decimal precioTotalProductoAEliminar = (bool)registroProducto.producto.producto_Esdescuento! ? registroProducto.producto.producto_precioDescuento : registroProducto.producto.producto_precio;

                    carrito.carrito_precioTotal -= precioTotalProductoAEliminar;
                    registroProducto.carritoCompra_cantidad -= 1;

                    _unitOfWork.Carrito.Update(carrito);
                    _unitOfWork.CarritoCompra.Update(registroProducto);
                    _unitOfWork.Save();
                    
                    mensaje = "cantidad eliminada exitosamente";
                }

                CarritoViewModel carritoViewModel = InformacionCarritoCompra(((ClaimsIdentity)this.User.Identity!), Request, _dataSecurity, _unitOfWork); ;
                
                _unitOfWork.Dispose();

                return StatusCode(200, new { 
                    data = mensaje,
                    precioTotal = carritoViewModel.precioTotal,
                    preciosProductos = carritoViewModel.productosPreciosConfigurados
                });
            }
            catch( Exception error )
            {
                return StatusCode(500, new { data = "Intentalo otra vez" });
            }
        }
        #endregion
    }
}
