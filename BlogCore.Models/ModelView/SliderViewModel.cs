using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.ModelView
{
    public class SliderViewModel
    {
        [ValidateNever]
        public string metodo { set; get; } = null!;

        public Slider slider { set; get; } = null!;

        [ValidateNever]
        public string texto { set; get; } = null!;

        [Display(Name = "Ingresa una imagen")]
        [ValidateNever]
        public IFormFile fileName { set; get; } = null!;
    }
}
