using e_commerce.Models.Repositories;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorieRepository _categorieRepository;

        public CategorieController(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        public IActionResult Index()
        {
            var categories = _categorieRepository.GetAll();
            return View(categories);
        }

      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categorie category)
        {
            if (ModelState.IsValid)
            {
                _categorieRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categorieRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categorie category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _categorieRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categorieRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categorieRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
