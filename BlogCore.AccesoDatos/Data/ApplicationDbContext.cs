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
    }
}
