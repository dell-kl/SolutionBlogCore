using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ModelView
{
    public class ComentarioProductoViewModel
    {
        [ValidateNever]
        public Producto producto { set; get; } = new Producto();
        
        public IEnumerable<OpinionesProducto> ListadoOpinionesProducto { set; get; } = new List<OpinionesProducto>();
        public IEnumerable<ComentarioProducto> ListadoComentarioProducto { set; get; } = new List<ComentarioProducto>();

        [Required(ErrorMessage = "Debes introducir un comentario del producto")]
        [Display(Name = "Aporta con un comentario al producto")]
        public string comentarioProducto { set; get; } = null!;
    }
}
