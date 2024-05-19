using e_commerce.Models.Repositories;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    [Authorize]
    public class PanierController : Controller
    {
        private readonly IPanierRepository _panierRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;
        public PanierController(IPanierRepository panierRepository, UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _panierRepository = panierRepository;
            _userManager = userManager;
            _context = context;
        }

        private string PanierID
        {
            get
            {
                // Vous pouvez utiliser la session ou un cookie pour stocker et récupérer l'ID du panier
                if (HttpContext.Session.GetString("PanierID") == null)
                {
                    // Générer un nouvel ID si nécessaire
                    var newPanierID = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("PanierID", newPanierID);
                }
                return HttpContext.Session.GetString("PanierID");
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtenir l'utilisateur actuellement connecté
                var user = await _userManager.GetUserAsync(User);

                // Vérifier si l'utilisateur est connecté
                if (user == null)
                {
                    // Gérer le cas où l'utilisateur n'est pas authentifié
                    return RedirectToAction("Login", "Account");
                }

                // Récupérer l'ID de l'utilisateur
                var userId = user.Id;

                // Récupérer les lignes du panier pour l'utilisateur connecté depuis le repository
                var lignesPanier = _panierRepository.GetLignesPanier(userId);

                // Retourner la vue avec les lignes de panier
                return View(lignesPanier);
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter(int articleId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User); // Obtenir l'utilisateur actuellement authentifié
                var userId = user?.Id; // Obtenir l'ID de l'utilisateur


                LignePanier ligne = _panierRepository.GetLignePanier(PanierID, articleId);

                if (ligne == null)
                {
                    ligne = new LignePanier
                    {
                        PanierId = PanierID,
                        UserID = userId,
                        ArticleID = articleId,
                        Qte = 1,

                    };
                    _panierRepository.AjouterLigne(ligne);
                }
                else
                {
                    ligne.Qte++;
                    _panierRepository.MettreAJourLigne(ligne);
                }

               
                return RedirectToAction("Index", "Panier");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }
        


    }
}
