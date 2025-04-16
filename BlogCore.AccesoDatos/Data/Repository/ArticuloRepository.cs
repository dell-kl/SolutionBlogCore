using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(
            ApplicationDbContext db    
        ) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> obtenerCategorias() => _db.Categoria.Select(n => new SelectListItem() {
                Text = n.categoria_nombre,
                Value = n.categoria_id.ToString()
        }).ToList();


        public void Update(Articulo articulo) => this._db.Update( articulo );
    }
}
