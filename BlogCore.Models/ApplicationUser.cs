using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Debes introducir un numero de cedula")]
        [Display(Name = "Cedula")]
        public string Cedula { set; get; } = null!;

        [Required(ErrorMessage = "Introduce un nombre")]
        [Display(Name = "Nombre")]
        public string Nombres { set; get; } = null!;

        [Required(ErrorMessage = "Introduce un email")]
        [Display(Name = "Email")]
        public string Email { set; get; } = null!;

        //relacion con reaccion Articulo
        public ICollection<ReaccionArticulo> reaccionArticulo = new List<ReaccionArticulo>();
    }
}
