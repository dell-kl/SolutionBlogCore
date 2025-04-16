using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ModelView
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> sliders { set; get; } = null!;
        public IEnumerable<Articulo> articulos { set; get; } = null!;
    }
}
