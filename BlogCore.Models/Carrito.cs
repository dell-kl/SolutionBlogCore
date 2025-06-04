using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class Carrito
    {
        [Key]
        public int carrito_id { set; get; }
        public string carrito_sessionId { set; get; } = null!;
        public DateTime carrito_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime carrito_fechaModificacion { set; get; } = DateTime.Now;

        //este precio total sera el que nos servira para procesar y generar una orden.
        public decimal carrito_precioTotal { set; get; } = 0.00M;

        [Column("carrito_usuarioId")]
        public string? IdentityUserId { set; get; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? identityUser { set; get; } = null;

        public ICollection<CarritoCompra> carritoCompra { set; get; } = new List<CarritoCompra>();
    }
}
