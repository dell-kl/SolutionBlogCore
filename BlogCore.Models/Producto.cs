using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class Producto
    {
        [Key]
        [Display(Name = "Identificador")]
        public int producto_id { set; get; }

        [Required(ErrorMessage = "Es necesario un nombre para el producto")]
        [Display(Name = "Nombre Producto")]
        public string producto_nombre { set; get; } = null!;

        [Required(ErrorMessage = "Introduce un precio para el producto")]
        [Display(Name = "Precio Producto")]
        public decimal producto_precio { set; get; }

        [Required(ErrorMessage = "Introduce un stock para el producto")]
        [Display(Name = "Stock Producto")]
        public int producto_stock { set; get; }

        [Required(ErrorMessage = "Inserta un estado para el producto")]
        [Display(Name = "Estado Producto")]
        public bool? producto_disponiblidad { set; get; } // si ya no hay stock se pone en falso

        [Required(ErrorMessage = "Ingresa una descripcion del producto")]
        [Display(Name = "Descripcion Producto")]
        public string producto_descripcion { set; get; } = null!;

        [Required(ErrorMessage = "Da un estado al producto, si aplica o no a descuento")]
        [Display(Name = "Aplica Descuento")]
        public bool? producto_Esdescuento { set; get; } = false;

        [Required(ErrorMessage = "Inserta un descuento, si no aplica descuento dejalo en 0")]
        [Display(Name = "Valor Descuento")]
        public int producto_descuento { set; get; } = 0;

        //este precio se va a calcular solo... crearemos nosotros un disparador.
        public decimal producto_precioDescuento { set; get; } = 0;
        
        public DateTime producto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime producto_fechaModificacion { set; get; } = DateTime.Now;

        [Column("producto_categoriaProductoId")]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Debes insertar una categoria")]
        public int CategoriaProductocategoriaProducto_id { set; get; }

        [ForeignKey("CategoriaProductocategoriaProducto_id")]
        [ValidateNever]
        public CategoriaProducto categoriaProducto { set; get; } = null!;

        public ICollection<ImagenesProducto> imagenesProducto { set; get; } = new List<ImagenesProducto>();
        public ICollection<VideosProducto> videosProducto { set; get; } = new List<VideosProducto>();
        public ICollection<OpinionesProducto> opinionesProducto { set; get; } = new List<OpinionesProducto>();

    }
}
