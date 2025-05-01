using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class VideosProducto
    {
        [Key]
        public int videosProducto_id { set; get; }
        public string videosProducto_ruta { set; get; } = null!;
        public bool videosProducto_estado { set; get; } = true;
        public DateTime videosProducto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime videosProducto_fechaModificacion { set; get; } = DateTime.Now;

        [Column("videosProducto_ProductoId")]
        public int Productoproducto_id { set; get; }

        [ForeignKey("Productoproducto_id")]
        [ValidateNever]
        public Producto producto { set; get; } = null!;
    }
}
    