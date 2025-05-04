using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.ModelView
{
    public class ProductoViewModel
    {
        [ValidateNever]
        public string metodo { set; get; } = null!;

        public Producto producto { set; get; } = null!;

        [ValidateNever]
        public string texto { set; get; } = null!;

        [Display(Name = "Imagenes Producto")]
        [ValidateNever]
        public IFormFileCollection filesName { set; get; } = null!;

        [Display(Name = "Videos Producto")]
        [ValidateNever]
        public IFormFileCollection videosName { set; get; } = null!;

        [Display(Name = "Categoria Productos")]
        public IEnumerable<SelectListItem> lstCategoriaProductos { set; get; } = new List<SelectListItem>();
    }
}
