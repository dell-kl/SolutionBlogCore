using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ModelView
{
    public class CategoriaProductoViewModel
    {
        public CategoriaProducto categoriaProducto { set; get; } = null!;

        public string mensajeBoton { set; get; } = null!;

        public string metodo { set; get; } = null!;
    }
}
