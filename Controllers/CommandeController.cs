using e_commerce.Migrations;
using e_commerce.Models;
using e_commerce.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommandeController : Controller
    {
        

        private readonly IReposetoryCommande _repositoryCommande;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPanierRepository _panierRepository;

        public CommandeController(IReposetoryCommande repositoryCommande, UserManager<IdentityUser> userManager, IPanierRepository panierRepository)
        {
            _repositoryCommande = repositoryCommande;
            _userManager = userManager;
            _panierRepository = panierRepository;
        }

        // Action to display the list of orders
        public IActionResult Index()
        {
            var commandes = _repositoryCommande.GetCommandes();
            return View(commandes);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Ajoute()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User); // Récupérer l'utilisateur actuellement authentifié
                if (user == null)
                {
                    // Gérer le cas où l'utilisateur n'est pas connecté
                    return RedirectToAction("Login", "Account");
                }

                var userId = user.Id;

                var commande = new Commande
                {
                    UtilisateurId = userId,
                    DateCommande = DateTime.Now,  // Valeur par défaut pour la date
                    EstPayee = false,             // Valeur par défaut pour le statut de paiement
                    EstLivree = false             // Valeur par défaut pour le statut de livraison
                };

                // Ajouter la commande à la base de données
                _repositoryCommande.AjouterCommande(commande);

                // Vider le panier de l'utilisateur après avoir ajouté la commande
                _panierRepository.ViderPanier(userId);

                // Rediriger vers la page de confirmation de commande ou une autre page
                return RedirectToAction("Confirmation", new { id = commande.CommandeId });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        [AllowAnonymous]
        public ActionResult Confirmation(int id)
        {
            // Récupérer les détails de la commande à partir de l'identifiant
            var commande = _repositoryCommande.GetCommandeById(id);

            if (commande == null)
            {
                // Gérer le cas où la commande n'est pas trouvée
                return NotFound();
            }

            // Passer les détails de la commande à la vue de confirmation
            return View(commande);
        }
        [AllowAnonymous]
        public async Task<IActionResult> MesCommandes()
        {
            var user = await _userManager.GetUserAsync(User); // Récupérer l'utilisateur actuellement authentifié
            if (user == null)
            {
                // Gérer le cas où l'utilisateur n'est pas connecté
                return RedirectToAction("Login", "Account");
            }

            var userId = user.Id;

            // Récupérer les commandes non livrées de l'utilisateur connecté
            var commandesNonLivre = _repositoryCommande.GetCommandesNonLivre(userId);

            return View(commandesNonLivre);
        }
        public IActionResult ClientsPlusFideles()
        {
            var clientsPlusFideles = _repositoryCommande.GetClientsPlusFideles();
            return View(clientsPlusFideles);
        }
    }
}
