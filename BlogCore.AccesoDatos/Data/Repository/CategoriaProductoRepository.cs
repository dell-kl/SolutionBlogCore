using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class CategoriaProductoRepository : Repository<CategoriaProducto>, ICategoriaProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CategoriaProducto categoriaProducto)
        {
           CategoriaProducto RegistrocategoriaProducto =  _db.CategoriaProducto.Where(n => n.categoriaProducto_guid.Equals(categoriaProducto.categoriaProducto_guid)).FirstOrDefault();
        
            if ( RegistrocategoriaProducto is not null )
            {
                RegistrocategoriaProducto.categoriaProducto_fechaModificacion = DateTime.Now;
                RegistrocategoriaProducto.categoriaProducto_nombre = categoriaProducto.categoriaProducto_nombre;

                _db.CategoriaProducto.Update(RegistrocategoriaProducto);
            }
        }
    }
}
