using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class Carrito
    {
        public int carrito_id { set; get; }
        public string carrito_sessionId { set; get; } = null!;
        public DateTime carrito_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime carrito_fechaModificacion { set; get; } = DateTime.Now;

        [Column("carrito_usuarioId")]
        public int IdentityUserId { set; get; }

        [Column("carrito_carritoCompraId")]
        public int CarritoCompracarritoCompra_id { set; get; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? identityUser { set; get; } = null;

        [ForeignKey("CarritoCompracarritoCompra_id")]
        public CarritoCompra carritoCompra { set; get; } = null!;
    }
}
