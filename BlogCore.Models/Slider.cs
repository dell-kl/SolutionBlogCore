using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int slider_id { set; get; }

        [Display(Name = "Ingresa un nombre")]
        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string slider_nombre { set; get; } = null!;

        [Display(Name = "Establece el estado")]
        [Required(ErrorMessage = "Debes introducir un estado")]
        public bool? slider_estado { set; get; }

        [DataType(DataType.ImageUrl)]
        [ValidateNever]
        public string slider_rutaImagen { set; get; } = null!;
    }
}
