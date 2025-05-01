using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class EtiquetaArticulo
    {
        [Column("EtiquetaArticulo_articuloId")]
        public int Articuloarticulo_id { set; get; }

        [Column("EtiquetaArticulo_etiquetaId")]
        public int Etiquetaetiqueta_id { set; get; }

        public Articulo articulo { set; get; } = null!;

        public Etiqueta etiqueta { set; get; } = null!;
    }
}
