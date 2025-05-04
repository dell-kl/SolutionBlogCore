using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IImagenesProductoRepository : IRepository<ImagenesProducto>
    {
        public void Update(int id);
        public void Update(ImagenesProductoRepository imagenProducto);
    }
}
