namespace e_commerce.Models.Repositories
{
    public interface IPanierRepository
    {

        LignePanier GetLignePanier(string panierId, int articleId);

        IEnumerable<LignePanier> GetLignesPanier(string userId);
        void AjouterLigne(LignePanier lignePanier);
        void MettreAJourLigne(LignePanier lignePanier);
        void ViderPanier(string userId);
        void Sauvegarder();
    }
}
