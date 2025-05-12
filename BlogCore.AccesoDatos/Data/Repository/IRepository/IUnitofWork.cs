namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        public ICategoriaRepository Categoria { get; }

        public IArticuloRepository Articulo { get; }

        public ISliderRepository Slider { get; }

        public IUsuarioRepository Usuario { get; }

        public IProductoRepository Producto { get;  }

        public ICategoriaProductoRepository CategoriaProducto { get; }

        public IImagenesProductoRepository ImagenesProducto { get; }

        public IVideoRepository Video { get; }

        public IComentarioArticuloRepository ComentarioArticulo { get; }

        public IOpinionesProductoRepository OpinionesProducto { get; }
        public void Save();
    }
}
