using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class CarritoComprasController : Controller
    {
        private readonly IDataSecurityRepository _dataSecurity;
        private readonly IUnitofWork _unitOfWork;
        public const string ID_COOKIE_CARRITO = "Carritos_Compras";

        public CarritoComprasController(IDataSecurityRepository dataSecurity, IUnitofWork unitOfWork)
        {
            _dataSecurity = dataSecurity;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CarritoViewModel carritoViewModel = new CarritoViewModel();
            //hacer persistente la cookie 
            string? cookieCarrito = Request.Cookies["Carritos_Compras"];

            if ( !cookieCarrito.IsNullOrEmpty() )
            {

                string CookieTextoPlano = _dataSecurity.desencriptarDatos(cookieCarrito!);
                carritoViewModel.carrito = _unitOfWork.Carrito.GetFirstOrDefault(n => n.carrito_sessionId.Equals(CookieTextoPlano));
                carritoViewModel.carritoCompra = _unitOfWork.CarritoCompra.GetAll(n => n.Carritocarrito_id.Equals(carritoViewModel.carrito.carrito_id));


                foreach (var item in carritoViewModel.carritoCompra)
                {
                    
                    Producto producto = _unitOfWork.Producto.GetFirstOrDefault(n => n.producto_id.Equals(item.Productoproducto_id), includeProperties: "imagenesProducto");

                    carritoViewModel.producto.Append(producto);
                }

            }

            return View(carritoViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string registro_aWQgZGVsIHByb2R1Y3RvCg, string cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y)
        {
            CookieOptions cookieOptiones = new CookieOptions() { 
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1),
                Path = "/"
            };

            string IDCarritoCookie = $"ID_SESSION_{Guid.NewGuid().ToString()}";
            string IDCarritoCookieEncriptado = _dataSecurity.encriptarDatos(IDCarritoCookie);
            string idProducto = _dataSecurity.desencriptarDatos(registro_aWQgZGVsIHByb2R1Y3RvCg);
            string cantidadProducto = _dataSecurity.desencriptarDatos(cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y);

            Carrito carrito = new Carrito()
            {
                carrito_sessionId = IDCarritoCookie
            };

            var claimsIdentity = ((ClaimsIdentity)this.User.Identity!);

            if (claimsIdentity.IsAuthenticated )
                carrito.IdentityUserId = claimsIdentity.Claims.ToList()[0].Value;
            
            _unitOfWork.Carrito.Add(carrito);
            _unitOfWork.Save();

            _unitOfWork.CarritoCompra.Add(new CarritoCompra()
            {
                Productoproducto_id = int.Parse(idProducto),
                carritoCompra_cantidad = int.Parse(cantidadProducto),
                Carritocarrito_id = _unitOfWork.Carrito.GetFirstOrDefault(c => c.carrito_sessionId.Equals(IDCarritoCookie)).carrito_id
            });
            _unitOfWork.Save();
            _unitOfWork.Dispose();

            Response.Cookies.Append(ID_COOKIE_CARRITO, IDCarritoCookieEncriptado, cookieOptiones);
            return RedirectToAction("Index");
        }
    }
}
