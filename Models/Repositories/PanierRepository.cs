using e_commerce.Migrations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Models.Repositories
{
    public class PanierRepository : IPanierRepository
    {
        private readonly AppDbContext _context;

        public PanierRepository(AppDbContext context)
        {
            _context = context;
        }
         
        public LignePanier GetLignePanier(string panierId, int articleId)
        {
            return _context.LignePanier.SingleOrDefault(s => s.PanierId == panierId && s.ArticleID == articleId);
        }

        public IEnumerable<LignePanier> GetLignesPanier(string userId)
        {
            return _context.LignePanier
                           .Where(lp => lp.UserID == userId)
                           .Include(lp => lp.Article)  // Ensure the related Article is included
                           .ToList();
        }



        public void AjouterLigne(LignePanier lignePanier)
        {
            _context.LignePanier.Add(lignePanier);
            _context.SaveChanges();
        }

        public void MettreAJourLigne(LignePanier lignePanier)
        {
            _context.LignePanier.Update(lignePanier);
            _context.SaveChanges();
        }

        public void Sauvegarder()
        {
            _context.SaveChanges();
        }
        public void ViderPanier(string userId)
        {
            // Récupérer toutes les lignes de panier de l'utilisateur
            var lignesPanier = _context.LignePanier.Where(l => l.UserID == userId).ToList();

            // Supprimer toutes les lignes de panier de l'utilisateur
            _context.LignePanier.RemoveRange(lignesPanier);

            // Enregistrer les modifications dans la base de données
            _context.SaveChanges();
        }
    }


}
