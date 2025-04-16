using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int articulo_id { set; get; }

        [Required(ErrorMessage = "Debes introducir un nombre")]
        [Display(Name = "Nombre del Articulo")]
        public string articulo_nombre { set; get; } = null!;

        [Required(ErrorMessage = "Debes ingresar una descripcion")]
        [Display(Name = "Descripcion del Articulo")]
        public string articulo_descripcion { set; get; } = null!;

        [Display(Name = "Fecha Creacion")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Debes ingresar una fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? articulo_fechaCreacion { set; get; }

        [DataType(DataType.ImageUrl)]
        
        public string? articulo_rutaImagen { set; get; }

        [Column("articulo_categoriaId")]
        [Display(Name = "Ingresa una categoria")]
        [Required(ErrorMessage = "Ingresa una categoria")]
        public int? Categoriacategoria_id { set; get; }

        [ForeignKey("Categoriacategoria_id")]
        [ValidateNever]
        public Categoria categoria { set; get; } = null!;
    }
}
