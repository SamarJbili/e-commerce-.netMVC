namespace e_commerce.Models.Repositories
{
    public class ReposetoryCommande : IReposetoryCommande
    {
        private readonly AppDbContext _context;

        public ReposetoryCommande(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Commande> GetCommandes()
        {
            return _context.Commandes.ToList();
        }

        public Commande GetCommandeById(int id)
        {
            return _context.Commandes.FirstOrDefault(c => c.CommandeId == id);
        }

        public void AjouterCommande(Commande commande)
        {
            commande.DateCommande = DateTime.Now;
            _context.Commandes.Add(commande);
            _context.SaveChanges();
        }
        public IEnumerable<Commande> GetCommandesNonLivre(string userId)
        {
            // Récupérer les commandes non livrées de l'utilisateur spécifié
            return _context.Commandes
                .Where(c => c.UtilisateurId == userId && !c.EstLivree)
                .ToList();
        }

        public void MettreAJourCommande(Commande commande)
        {
            _context.Commandes.Update(commande);
            _context.SaveChanges();
        }

        public void SupprimerCommande(Commande commande)
        {
            _context.Commandes.Remove(commande);
            _context.SaveChanges();
        }
        // Exemple de méthode dans votre repository de commandes

        public IEnumerable<AppDbContext> GetClientsPlusFideles()
        {
            return _context.Commandes
                           .Where(c => c.EstPayee && c.EstLivree) // Filtre pour les commandes payées et livrées
                           .GroupBy(c => c.Utilisateur)
                           .OrderByDescending(g => g.Sum(c => c.MontantTotal) // Trier par le montant total des achats
                           .Select(g => g.Key)
                           .Take(10) // Prendre les 10 clients les plus fidèles, par exemple
                           .ToList();
        }

    }
}
