using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Categoria> Categoria { set; get; }
        public DbSet<Articulo> Articulo { set; get; }
        public DbSet<Slider> Slider { set; get; }
        public DbSet<ApplicationUser> ApplicationUser { set; get; }
        public DbSet<Producto> Producto { set; get; }
        public DbSet<ImagenesProducto> ImagenesProducto { set; get; }
        public DbSet<VideosProducto> VideosProducto { set; get; }
        public DbSet<OpinionesProducto> OpinionesProducto { set; get; }
        public DbSet<CategoriaProducto> CategoriaProducto { set; get; }
        public DbSet<ReaccionArticulo> ReaccionArticulo { set; get; }
        public DbSet<EtiquetaArticulo> EtiquetaArticulo { set; get; }
        public DbSet<Etiqueta> Etiqueta { set; get; }
        public DbSet<ComentarioArticulo> ComentarioArticulo { set; get; }
        public DbSet<ComentarioProducto> ComentarioProducto { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ReaccionArticulo>()
                .HasNoKey();
    
            builder.Entity<EtiquetaArticulo>()
                .HasNoKey();

            //relacion recursiva
            builder.Entity<ComentarioArticulo>(entity =>
            {
                entity
                .HasOne(n => n.ComentarioArticulocomentarioArticuloFK)
                .WithMany(n => n.listadoComentarioArticulos)
                .HasForeignKey(fk => fk.ComentarioArticulocomentarioArticulo_id)
                .HasConstraintName("FK__Comentari__comen__4AB81AF0");
                //.OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ComentarioProducto>(entity =>
            {
                entity
                .HasOne(n => n.comentarioProductoFK)
                .WithMany(n => n.listadoComentarioProductos)
                .HasForeignKey(fk => fk.ComentarioProductocomentarioProducto_id)
                .HasConstraintName("FK_Comentario_product_4AB81AF0");
            });    

            base.OnModelCreating(builder);
        }
    }
}
