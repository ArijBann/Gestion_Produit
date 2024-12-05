using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionDesProduits.Models;

namespace GestionDesProduits.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GestionDesProduits.Models.Magasin>? Magasin { get; set; }
        public DbSet<GestionDesProduits.Models.Categorie>? Categorie { get; set; }
        public DbSet<GestionDesProduits.Models.Produits>? Produits { get; set; }



    }
}