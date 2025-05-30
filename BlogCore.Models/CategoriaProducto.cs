﻿using Microsoft.AspNetCore.Mvc.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class CategoriaProducto
    {
        [Key]
        public int categoriaProducto_id { set; get; }
        public Guid categoriaProducto_guid { set; get; } = Guid.NewGuid();

        [Required(ErrorMessage = "Debes introducir un nombre a la categoria")]
        [Display(Name = "Nombre Categoria")]
        public string categoriaProducto_nombre { set; get; } = null!;
        public DateTime categoriaProducto_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime categoriaProducto_fechaModificacion { set; get; } = DateTime.Now;
    
        public ICollection<Producto> productos { set; get; } = new List<Producto>();
    }
}
