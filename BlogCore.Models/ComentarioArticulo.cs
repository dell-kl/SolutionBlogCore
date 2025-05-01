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
    public class ComentarioArticulo
    {
        [Key]
        public int comentarioArticulo_id { set; get; }
        public string comentarioArticulo_descripcion { set; get; } = null!;
        public DateTime comentarioArticulo_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime comentarioArticulo_fechaModificacion { set; get; } = DateTime.Now;

        [Column("comentarioArticulo_articuloId")]
        public int Articuloarticulo_id { set; get; }

        [ForeignKey("Articuloarticulo_id")]
        [ValidateNever]
        public Articulo articulo { set; get; } = null!;
    }
}
