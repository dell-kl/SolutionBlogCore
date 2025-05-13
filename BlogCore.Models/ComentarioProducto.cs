using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class ComentarioProducto
    {
        [Key]
        public int comentarioProducto_id { set; get; }
        public Guid comentarioProducto_guid { set; get; } = Guid.NewGuid();
        public string comentarioProducto_nombrePublicador { set; get; } = null!;
        public string comentarioProducto_descripcion { set; get; } = null!;
        public DateTime comentarioProducto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime comentarioProducto_fechaModificacion { set; get; } = DateTime.Now;
        
        [Column("comentarioProducto_productoId")]
        public int Productoproducto_id { set; get; }

        [ForeignKey("Productoproducto_id")]
        [ValidateNever]
        public Producto producto { set; get; } = null!;

        /* Configuracion para la parte de la relacion recursiva */
        [Column("comentarioProducto_CmtProductFkID")]
        public int? ComentarioProductocomentarioProducto_id { set; get; }
        public virtual ComentarioProducto? comentarioProductoFK { set; get; }
        public ICollection<ComentarioProducto> listadoComentarioProductos = new List<ComentarioProducto>();
    }
}
