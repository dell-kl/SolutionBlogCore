using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoriaRepository Categoria {  get; private set; }

        public IArticuloRepository Articulo { get; private set; }

        public ISliderRepository Slider { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }

        public UnitofWork(ApplicationDbContext db)
        {
            this._db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Slider = new SliderRepository(_db);
            Usuario = new UsuarioRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
