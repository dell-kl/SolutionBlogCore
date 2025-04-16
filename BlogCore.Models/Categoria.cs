using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int categoria_id { set; get; }

        [Required(ErrorMessage = "Ingresa un nombre para la categoria")]
        [Display(Name = "Nombre de Categoria")]
        public string categoria_nombre { get; set; }

        [Display(Name = "Orden de Visualizacion")]
        [Required(ErrorMessage = "Ingresa un orden de visualizacion")]
        [Range(1, 100, ErrorMessage = "El orden de visualizacion debe ser entre 1 y 100")]
        public int? categoria_orden { set; get; }

        public ICollection<Articulo> articulos { set; get; } = new List<Articulo>();
    }
}
