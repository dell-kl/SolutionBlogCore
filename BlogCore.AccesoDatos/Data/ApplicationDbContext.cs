using BlogCore.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<ComentarioArticulo> ComentarioArticulo { set; get; }
        public DbSet<ReaccionArticulo> ReaccionArticulo { set; get; }
        public DbSet<EtiquetaArticulo> EtiquetaArticulo { set; get; }
        public DbSet<Etiqueta> Etiqueta { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<ReaccionArticulo>()
                .HasNoKey();
    
            builder.Entity<EtiquetaArticulo>()
                .HasNoKey();


            base.OnModelCreating(builder);
        }
    }
}
