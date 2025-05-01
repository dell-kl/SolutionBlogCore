using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class OpinionesProducto
    {
        [Key]
        public int opinionesProducto_id { set; get; }
        public string opinionesProducto_tema { set; get; } = null!;
        public string opinionesProducto_descripcion { set; get;} = null!;
        public string opinionesProducto_recursoRuta { set; get; } = null!;
        public int opinionesProducto_puntuacion { set; get; }
        public DateTime opinionesProducto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime opinionesProducto_fechaModificacion { set; get; } = DateTime.Now;

        [Column("opinionesProducto_productoId")]
        public int Productoproducto_id { set; get; }

        [ForeignKey("Productoproducto_id")]
        [ValidateNever]
        public Producto producto { set; get; } = null!;
    }
}
