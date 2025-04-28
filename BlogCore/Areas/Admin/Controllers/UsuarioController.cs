using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public UsuarioController(IUnitofWork unitofWork) {
            _unitofWork = unitofWork;
        }

        // este permiso permite acceder a la vista sin necesidad de estar logueado.
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> usuarios = new List<ApplicationUser>();
            var n = (ClaimsIdentity) this.User.Identity!;
            if(n.IsAuthenticated)
            {
                var u = n.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                usuarios = _unitofWork.Usuario.GetAll(n=>!n.Id.Equals(u));
            }
            else
                usuarios = _unitofWork.Usuario.GetAll();

            return View(usuarios);
        }

        [HttpGet]
        public IActionResult BloquearCuenta(string idCuenta)
        {
            _unitofWork.Usuario.bloquearCuenta(idCuenta);
            _unitofWork.Save();
            _unitofWork.Dispose();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DesbloquearCuenta(string idCuenta)
        {
            _unitofWork.Usuario.desbloquearCuenta(idCuenta);
            _unitofWork.Save();
            _unitofWork.Dispose();

            return RedirectToAction("Index");
        }
    }
}
