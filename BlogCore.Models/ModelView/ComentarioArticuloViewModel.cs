using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ModelView
{
    public class ComentarioArticuloViewModel
    {
        [ValidateNever]
        public Articulo articulo { set; get; } = new Articulo();

        public IEnumerable<ComentarioArticulo> listadoComentariosArticulo = new List<ComentarioArticulo>();
        
        [Required(ErrorMessage = "Para aportar debes ingresar tu opinion, caso contrario no se podra postear.")]
        [MaxLength(500, ErrorMessage = "Tienes un maximo de 500 palabras para escribir tu opinion, sugerencia o idea.")]
        [Display(Name = "Puedes hacer un comentario sobre como te parecio este articulo")]
        [DataType(DataType.Text)]
        public string comentarioArticulo { set; get; } = null!;

    }
}
