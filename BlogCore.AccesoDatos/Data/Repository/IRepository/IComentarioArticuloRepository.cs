using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IComentarioArticuloRepository : IRepository<ComentarioArticulo>
    {
        public void Update(ComentarioArticulo comentarioArticulo);
    }
}
