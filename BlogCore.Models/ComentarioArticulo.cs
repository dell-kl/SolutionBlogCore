using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class ComentarioArticulo
    {
        [Key]
        public int comentarioArticulo_id { set; get; }
        public Guid comentarioArticulo_guid { set; get; } = Guid.NewGuid();
        public string comentarioArticulo_nombrePublicador { set; get; } = null!;
        public string comentarioArticulo_descripcion { set; get; } = null!;
        public DateTime comentarioArticulo_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime comentarioArticulo_fechaModificacion { set; get; } = DateTime.Now;

        [Column("comentarioArticulo_articuloId")]
        public int Articuloarticulo_id { set; get; }

        [ForeignKey("Articuloarticulo_id")]
        [ValidateNever]
        public Articulo articulo { set; get; } = null!;

        /* Configuracion para la parte de la relacion recursiva */
        [Column("comentarioArticulo_CmtArticulofkID")]
        public int? ComentarioArticulocomentarioArticulo_id { set; get; }
        public virtual ComentarioArticulo? ComentarioArticulocomentarioArticuloFK { set; get; }
        public ICollection<ComentarioArticulo> listadoComentarioArticulos = new List<ComentarioArticulo>();
    }
}
