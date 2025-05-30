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

        public IProductoRepository Producto { get; private set; }

        public ICategoriaProductoRepository CategoriaProducto { get; private set; }

        public IImagenesProductoRepository ImagenesProducto { get; private set; }
        
        public IVideoRepository Video { get; private set; }

        public IComentarioArticuloRepository ComentarioArticulo { get; private set; }

        public IOpinionesProductoRepository OpinionesProducto { get; private set; }

        public IComentarioProductoRepository ComentarioProducto { get; private set; }
        
        public ICarritoRepository Carrito { get; private set; }
        
        public ICarritoCompraRepository CarritoCompra { get; private set; }

        public UnitofWork(ApplicationDbContext db)
        {
            this._db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Slider = new SliderRepository(_db);
            Usuario = new UsuarioRepository(_db);
            Producto = new ProductoRepository(_db);
            CategoriaProducto = new CategoriaProductoRepository(_db);
            ImagenesProducto = new ImagenesProductoRepository(_db);
            Video = new VideoRepository(_db);
            ComentarioArticulo = new ComentarioArticuloRepository(_db);
            OpinionesProducto = new OpinionesProductoRepository(_db);
            ComentarioProducto = new ComentarioProductoRepository(_db);
            Carrito = new CarritoRepository(_db);
            CarritoCompra = new CarritoCompraRepository(_db);
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
