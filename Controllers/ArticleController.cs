using e_commerce.Models.Repositories;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace e_commerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment; 
        public ArticleController(IArticleRepository articleRepository, ICategorieRepository categorieRepository, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            this.articleRepository = articleRepository;
            _categorieRepository = categorieRepository;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
            
        public IActionResult Index()
        {

            var articles = articleRepository.GetAll();
            var CategorieId = _categorieRepository.GetAll();

            ViewBag.CategorieId = new SelectList(CategorieId, "Id", "Nom");

            return View(articles);

        }
        [AllowAnonymous]
        public IActionResult Index2()
        {

            var articles = articleRepository.GetAll();
            var CategorieId = _categorieRepository.GetAll();

            ViewBag.CategorieId = new SelectList(CategorieId, "Id", "Nom");

            return View(articles);

        }

        public IActionResult Details(int id)
        {
            var article = articleRepository.GetById(id);
            var CategorieId = _categorieRepository.GetAll();

            ViewBag.CategorieId = new SelectList(CategorieId, "Id", "Nom");
            return View(article);
        }

        public IActionResult Create()
        {
            var categories = _categorieRepository.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel model)
        {
            try
            {
                string uniqueFileName = null;
                if (model.ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Article newArticle = new Article
                {
                    name = model.name,
                    Description = model.Description,
                    Prix = model.Prix,
                    CategorieId = model.CategorieId,
                    ImagePath = uniqueFileName
                };

                articleRepository.Add(newArticle);
                var categories = _categorieRepository.GetAll();
                ViewBag.Categories = new SelectList(categories, "Id", "Nom", model.CategorieId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {


                return View();
            }
        }



        public IActionResult Edit(int id)
        {

            var article = articleRepository.GetById(id);
            
            var categories = _categorieRepository.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Nom", article.CategorieId);
            var editViewModel = new EditViewModel
            {
                Id = article.Id,
                name = article.name,
                Description = article.Description,
                Prix = article.Prix,
                CategorieId = article.CategorieId,
                ExistingImagePath = article.ImagePath
            };

            return View(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Edit(EditViewModel model)
        {
            try
            {
                var categories = _categorieRepository.GetAll();

                var article = articleRepository.GetById(model.Id);
                    if (article == null)
                    {
                        return NotFound();
                    }

                    // Mise à jour des propriétés de l'article avec les données du modèle ViewModel
                    article.name = model.name;
                    article.Description = model.Description;
                    article.Prix = model.Prix;
                    article.CategorieId = model.CategorieId;

                    // Si une nouvelle image est téléchargée, traiter le fichier téléchargé et mettre à jour le chemin de l'image
                    if (model.ImagePath != null)
                    {
                    ViewBag.Categories = new SelectList(categories, "Id", "Nom", article.CategorieId);
                    // Supprimer l'image existante si elle existe
                    if (!string.IsNullOrEmpty(model.ExistingImagePath))
                        {
                        
                        
                        string existingFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }
                        // Traiter et sauvegarder la nouvelle image téléchargée
                        article.ImagePath = ProcessUploadedFile(model);
                    
                }

                    // Mettre à jour l'article dans la base de données
                    articleRepository.Update(article);
                    ViewBag.Categories = new SelectList(categories, "Id", "Nom", article.CategorieId);
                    return RedirectToAction(nameof(Index));
                
            
            }
            catch
            {
                // En cas d'erreur, retournez la vue avec le modèle pour permettre à l'utilisateur de corriger
                return View(model);
            }
        }


        // Méthode pour traiter le fichier téléchargé et renvoyer son nom unique
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImagePath.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public IActionResult Delete(int id)
        {
            var article = articleRepository.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            articleRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
     
    }
}
