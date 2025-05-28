using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class CarritoCompra
    {
        [Key]
        public int carritoCompra_id { set; get; }
        public Guid carritoCompra_guid { set; get; } = Guid.NewGuid();
        public int carritoCompra_cantidad { set; get; }

        // fechas de creacion y modificacion
        public DateTime carritoCompra_creacion { set; get; } = DateTime.Now;
        public DateTime carritoCompra_modificacion { set; get; } = DateTime.Now;


        // relacion con otras tablas importantes.
        [Column("carritoCompra_productoId")]
        public int Productoproducto_id { set; get; }

        [Column("carritoCompra_carritoId")]
        public int Carritocarrito_id { set; get; }


        [ForeignKey("Productoproducto_id")]
        public Producto producto { set; get; } = null!;

        [ForeignKey("Carritocarrito_id")]
        public Carrito carrito { set; get; } = null!;
    }
}
