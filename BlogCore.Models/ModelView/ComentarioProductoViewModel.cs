using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ModelView
{
    public class ComentarioProductoViewModel
    {
        public Producto producto { set; get; } = new Producto();
        public IEnumerable<OpinionesProducto> ListadoOpinionesProducto { set; get; } = new List<OpinionesProducto>();
        
    }
}
