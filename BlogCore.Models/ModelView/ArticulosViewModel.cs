using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.ModelView
{
    public class ArticulosViewModel
    {
        [ValidateNever]
        public string metodo { set; get; } = null!;
        public Articulo articulo { set; get; } = null!;

        [ValidateNever]
        public string texto { set; get; } = null!;

        //[Required(ErrorMessage = "Debes ingresar una imagen")]
        [Display(Name = "Ingresa una Imagen")]
        [ValidateNever]
        public IFormFile fileName { set; get; } = null!;

        public IEnumerable<SelectListItem> listadoCategoria { set; get; } = new List<SelectListItem>();
    }
}
