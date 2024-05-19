using Microsoft.AspNetCore.Identity;

namespace e_commerce.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public string UtilisateurId { get; set; }
        public DateTime DateCommande { get; set; }
        public bool EstPayee { get; set; }
        public bool EstLivree { get; set; }

        // Relation avec l'utilisateur
        public IdentityUser Utilisateur { get; set; }

        // Liste des articles commandés
        public float MontantTotal { get; set; }
    }
}
