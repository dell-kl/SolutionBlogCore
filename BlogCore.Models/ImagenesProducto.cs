using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class ImagenesProducto
    {
        [Key]
        public int imagenesProducto_id { set; get; }
        public string imagenesProducto_ruta { set; get; } = null!;
        public bool imagenesProducto_estado { set; get; } = true;
        public DateTime imagenesProducto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime imagenesProducto_fechaModificacion { set; get; } = DateTime.Now;

        [Column("imagenesProducto_ProductoId")]
        public int Productoproducto_id { set; get; }

        [ForeignKey("Productoproducto_id")]
        [ValidateNever]
        public Producto producto { set; get; } = null!;
    }
}
