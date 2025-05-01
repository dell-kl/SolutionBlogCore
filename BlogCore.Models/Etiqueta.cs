using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Etiqueta
    {
        [Key]
        public int etiqueta_id { set; get; }
        public string etiqueta_nombre { set; get; } = null!;
        public string etiqueta_color { set; get; } = null!;
        public bool etiqueta_disponiblidad { set; get; }
        public DateTime etiqueta_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime etiqueta_fechaModificacion { set; get; } = DateTime.Now;
        public ICollection<EtiquetaArticulo> etiquetaArticulo = new List<EtiquetaArticulo>();
    }
}
