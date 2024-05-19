using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using e_commerce.ViewModels;
namespace e_commerce.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<LignePanier> LignePanier { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Commande> Commandes { get; set; }

      
        public DbSet<e_commerce.ViewModels.CreateRoleViewModel> CreateRoleViewModel { get; set; } = default!;
        public DbSet<e_commerce.ViewModels.EditRoleViewModel> EditRoleViewModel { get; set; } = default!;
        public DbSet<e_commerce.ViewModels.UserRoleViewModel> UserRoleViewModel { get; set; } = default!;

    }
}
