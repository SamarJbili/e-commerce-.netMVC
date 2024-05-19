namespace e_commerce.Models.Repositories
{
    public interface IReposetoryCommande
    {
        IEnumerable<Commande> GetCommandes();
        Commande GetCommandeById(int id);
        void AjouterCommande(Commande commande);
        void MettreAJourCommande(Commande commande);
        void SupprimerCommande(Commande commande);
        IEnumerable<Commande> GetCommandesNonLivre(string userId);
        IEnumerable<AppDbContext> GetClientsPlusFideles();

    }
}
