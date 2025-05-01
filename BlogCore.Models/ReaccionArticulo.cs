using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class ReaccionArticulo
    {
        //tabla compuesta para nuestro ApplicationUser y Articulo
        
        [Column("ReaccionComentario_articuloId")]
        public int Articuloarticulo_id { set; get; }

        [Column("ReaccionComentario_aspNetUserId")]
        public int ApplicationUserId { set; get; }

        public Articulo articulo { set; get; } = null!;

        public ApplicationUser applicationUser { set; get; } = null!;
    }
}
