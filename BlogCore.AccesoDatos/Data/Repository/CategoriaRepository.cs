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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;


        public CategoriaRepository(ApplicationDbContext db) : base(db) {
            this._db = db;
        }

        public void Update(Categoria categoria)
        {

            this._db.Update(
                categoria    
            );

        }
    }
}
