using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class CarritoComprasController : Controller
    {
        private readonly IDataSecurityRepository _dataSecurity;

        public CarritoComprasController(IDataSecurityRepository dataSecurity)
        {
            _dataSecurity = dataSecurity;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string registro_aWQgZGVsIHByb2R1Y3RvCg, string cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y)
        {
            try
            {
                //hacer una verificacion de si el usuario esta logueado.
                string idProducto = _dataSecurity.desencriptarDatos(registro_aWQgZGVsIHByb2R1Y3RvCg);
                string cantidadProducto = _dataSecurity.desencriptarDatos(cantidad_Y2FudGlkYWQgZGVsIHByb2R1Y);
            

            }
            catch(Exception e)
            {

            }

            return null;
        }
    }
}
